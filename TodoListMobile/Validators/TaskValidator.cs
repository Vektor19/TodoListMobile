using TodoListMobile.Constants;

namespace TodoListMobile.Validators
{
    public class TaskValidator
    {
        public bool IsTaskValid(string title, string description, DateTime dueDate)
        {
            return IsTitleValid(title) &&
                   IsDescriptionValid(description) &&
                   IsDueDateValid(dueDate);
        }

        public bool IsTitleValid(string title)
        {
            return !string.IsNullOrWhiteSpace(title) &&
                   title.Length <= ValidationConstants.MaxTitleLength;
        }

        public bool IsDescriptionValid(string description)
        {
            return !string.IsNullOrWhiteSpace(description) &&
                   description.Length <= ValidationConstants.MaxDescriptionLength;
        }

        public bool IsDueDateValid(DateTime dueDate)
        {
            return dueDate >= DateTime.Today;
        }
    }
}
