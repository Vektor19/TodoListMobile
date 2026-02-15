using System.Collections.ObjectModel;

namespace TodoListMobile.Models
{
    public static class TodoItemStore
    {
        public static ObservableCollection<TodoItem> Items { get; } = new()
        {
            new TodoItem
            {
                Title = "Buy groceries",
                Description = "Milk, eggs, bread",
                DueDate = DateTime.Today.AddDays(1)
            },
            new TodoItem
            {
                Title = "Finish report",
                Description = "Finalize the quarterly summary",
                DueDate = DateTime.Today.AddDays(3)
            },
            new TodoItem
            {
                Title = "Call plumber",
                Description = "Schedule a visit for the kitchen sink",
                DueDate = DateTime.Today.AddDays(2)
            }
        };
    }
}
