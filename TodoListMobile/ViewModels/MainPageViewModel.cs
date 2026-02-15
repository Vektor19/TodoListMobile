using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TodoListMobile.Validators;

namespace TodoListMobile.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly TaskValidator _validator;
        private string _title = string.Empty;
        private string _description = string.Empty;
        private DateTime _dueDate = DateTime.Today;

        public MainPageViewModel()
        {
            _validator = new TaskValidator();
            AddTaskCommand = new Command(async () => await OnAddTaskAsync(), () => IsAddButtonEnabled);
        }

        public string Title
        {
            get => _title;
            set
            {
                if (SetProperty(ref _title, value))
                {
                    ValidateAndUpdateButton();
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
                    ValidateAndUpdateButton();
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
                    ValidateAndUpdateButton();
                }
            }
        }

        public bool IsAddButtonEnabled => _validator.IsTaskValid(Title, Description, DueDate);

        public ICommand AddTaskCommand { get; }

        private void ValidateAndUpdateButton()
        {
            OnPropertyChanged(nameof(IsAddButtonEnabled));
            ((Command)AddTaskCommand).ChangeCanExecute();
        }

        private async Task OnAddTaskAsync()
        {
            var confirmation = await Application.Current.MainPage.DisplayAlert(
                "Confirm Task",
                $"Do you really want to add this task?\n\n" +
                $"Title: {Title}\n" +
                $"Description: {Description}\n" +
                $"Due Date: {DueDate:d}",
                "Yes",
                "No");

            if (confirmation)
            {
                // TODO: Add logic to save the task
                await Application.Current.MainPage.DisplayAlert("Success", "Task added successfully!", "OK");

                // Clear fields
                Title = string.Empty;
                Description = string.Empty;
                DueDate = DateTime.Today;
            }
        }
    }
}
