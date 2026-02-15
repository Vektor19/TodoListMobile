using System.Collections.ObjectModel;

namespace TodoListMobile.Models
{
    public static class TodoItemStore
    {
        public static ObservableCollection<TodoItem> Items { get; } = new();
    }
}
