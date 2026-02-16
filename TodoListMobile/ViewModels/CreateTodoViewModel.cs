using System.Threading.Tasks;
using System.Windows.Input;
using TodoListMobile.Models;
using TodoListMobile.Resources.Strings;
using TodoListMobile.Validators;

namespace TodoListMobile.ViewModels
{
    public class CreateTodoViewModel : BaseViewModel
    {
        private readonly TaskValidator _validator = new();
        private string _title = string.Empty;
        private string _description = string.Empty;
        private DateTime _dueDate = DateTime.Today;
        private string? _imageSource = "todolist_icon.png";
        private TodoItem? _editingItem;
        private bool _isEditing;

        public string Title
        {
            get => _title;
            set
            {
                if (SetProperty(ref _title, value))
                {
                    OnPropertyChanged(nameof(IsAddButtonEnabled));
                }
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (SetProperty(ref _description, value))
                {
                    OnPropertyChanged(nameof(IsAddButtonEnabled));
                }
            }
        }

        public DateTime DueDate
        {
            get => _dueDate;
            set
            {
                if (SetProperty(ref _dueDate, value))
                {
                    OnPropertyChanged(nameof(IsAddButtonEnabled));
                }
            }
        }

        public string? ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        public ICommand PickPhotoCommand { get; }

        public bool IsAddButtonEnabled => _validator.IsTaskValid(Title, Description, DueDate);

        public bool IsEditing
        {
            get => _isEditing;
            private set
            {
                if (SetProperty(ref _isEditing, value))
                {
                    OnPropertyChanged(nameof(AddButtonText));
                }
            }
        }

        public string AddButtonText => IsEditing ? AppResources.SaveChangesButton : AppResources.AddTaskButton;

        public CreateTodoViewModel()
        {
            PickPhotoCommand = new Command(async () => await PickPhotoAsync());
        }

        public async Task PickPhotoAsync()
        {
            try
            {
                await Permissions.RequestAsync<Permissions.Photos>();

                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Pick a photo"
                });

                if (result != null)
                {
                    var localFilePath = Path.Combine(FileSystem.AppDataDirectory, result.FileName);

                    using var stream = await result.OpenReadAsync();
                    using var newStream = File.OpenWrite(localFilePath);
                    await stream.CopyToAsync(newStream);

                    ImageSource = localFilePath;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    $"Failed to pick photo: {ex.Message}",
                    "OK");
            }
        }

        public void BeginEdit(TodoItem item)
        {
            _editingItem = item;
            Title = item.Title;
            Description = item.Description;
            DueDate = item.DueDate;
            ImageSource = string.IsNullOrEmpty(item.Image) ? "todolist_icon.png" : item.Image;
            IsEditing = true;
        }

        public async Task SaveAsync()
        {
            if (_editingItem is null)
            {
                var newItem = new TodoItem
                {
                    Title = Title,
                    Description = Description,
                    DueDate = DueDate,
                    Image = ImageSource != "todolist_icon.png" ? ImageSource : null
                };

                await TodoItemStore.AddOrUpdateAsync(newItem);
            }
            else
            {
                _editingItem.Title = Title;
                _editingItem.Description = Description;
                _editingItem.DueDate = DueDate;
                _editingItem.Image = ImageSource != "todolist_icon.png" ? ImageSource : null;
                await TodoItemStore.AddOrUpdateAsync(_editingItem);
                _editingItem = null;
                IsEditing = false;
            }
        }

        public void Reset()
        {
            Title = string.Empty;
            Description = string.Empty;
            DueDate = DateTime.Today;
            ImageSource = "todolist_icon.png";
            _editingItem = null;
            IsEditing = false;
        }
    }
}
