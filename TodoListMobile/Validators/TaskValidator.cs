namespace TodoListMobile.Validators
{
    public class TaskValidator
    {
        public bool IsTaskValid(string title, string description, DateTime dueDate)
        {
            return !string.IsNullOrWhiteSpace(title) &&
                   !string.IsNullOrWhiteSpace(description) &&
                   dueDate >= DateTime.Today;
        }

        public bool IsTitleValid(string title)
        {
            return !string.IsNullOrWhiteSpace(title);
        }

        public bool IsDescriptionValid(string description)
        {
            return !string.IsNullOrWhiteSpace(description);
        }

        public bool IsDueDateValid(DateTime dueDate)
        {
            return dueDate >= DateTime.Today;
        }
    }
}
