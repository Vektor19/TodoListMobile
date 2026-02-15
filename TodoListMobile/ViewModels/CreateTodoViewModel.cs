using TodoListMobile.Validators;

namespace TodoListMobile.ViewModels
{
    public class CreateTodoViewModel : BaseViewModel
    {
        private readonly TaskValidator _validator = new();
        private string _title = string.Empty;
        private string _description = string.Empty;
        private DateTime _dueDate = DateTime.Today;

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

        public void Reset()
        {
            Title = string.Empty;
            Description = string.Empty;
            DueDate = DateTime.Today;
        }
    }
}
