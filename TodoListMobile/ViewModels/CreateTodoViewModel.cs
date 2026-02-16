using System.Threading.Tasks;
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

        public void BeginEdit(TodoItem item)
        {
            _editingItem = item;
            Title = item.Title;
            Description = item.Description;
            DueDate = item.DueDate;
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
                    DueDate = DueDate
                };

                await TodoItemStore.AddOrUpdateAsync(newItem);
            }
            else
            {
                _editingItem.Title = Title;
                _editingItem.Description = Description;
                _editingItem.DueDate = DueDate;
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
            _editingItem = null;
            IsEditing = false;
        }
    }
}
