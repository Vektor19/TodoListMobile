namespace TodoListMobile.ViewModels
{
    public class TodoListPageViewModel : BaseViewModel
    {
        private string _title = string.Empty;
        private string _description = string.Empty;
        private DateTime _dueDate = DateTime.Today;

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
    }
}
