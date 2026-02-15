using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoListMobile.Models;

namespace TodoListMobile.ViewModels
{
    public class TodoListPageViewModel : BaseViewModel
    {
        public ObservableCollection<TodoItem> Items => TodoItemStore.Items;

        public ICommand EditCommand { get; }

        public ICommand DeleteCommand { get; }

        public TodoListPageViewModel()
        {
            EditCommand = new Command<TodoItem>(async item => await EditItemAsync(item));
            DeleteCommand = new Command<TodoItem>(DeleteItem);
        }

        private async Task EditItemAsync(TodoItem? item)
        {
            if (item is null)
            {
                return;
            }

            await Shell.Current.GoToAsync(nameof(CreateTodoPage), true, new Dictionary<string, object>
            {
                ["TodoItem"] = item
            });
        }

        private void DeleteItem(TodoItem? item)
        {
            if (item is null)
            {
                return;
            }

            TodoItemStore.Items.Remove(item);
        }
    }
}
