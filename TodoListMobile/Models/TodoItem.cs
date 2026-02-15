using TodoListMobile.ViewModels;

namespace TodoListMobile.Models
{
    public class TodoItem : BaseViewModel
    {
        private string _title = string.Empty;
        private string _description = string.Empty;
        private DateTime _dueDate = DateTime.Today;
        private string? _image;
        private TodoStatus _status = TodoStatus.Todo;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public DateTime DueDate
        {
            get => _dueDate;
            set => SetProperty(ref _dueDate, value);
        }

        public string? Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public TodoStatus Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }
    }
}
