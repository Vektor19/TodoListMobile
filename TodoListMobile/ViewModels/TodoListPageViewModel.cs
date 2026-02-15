using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoListMobile.Models;

namespace TodoListMobile.ViewModels
{
    public class TodoListPageViewModel : BaseViewModel
    {
        private bool _hideCompleted;

        public ObservableCollection<TodoItem> Items => TodoItemStore.Items;

        public ObservableCollection<TodoItem> VisibleItems { get; } = new();

        public ICommand EditCommand { get; }

        public ICommand DeleteCommand { get; }

        public bool HideCompleted
        {
            get => _hideCompleted;
            set
            {
                if (SetProperty(ref _hideCompleted, value))
                {
                    RefreshVisibleItems();
                }
            }
        }

        public TodoListPageViewModel()
        {
            EditCommand = new Command<TodoItem>(async item => await EditItemAsync(item));
            DeleteCommand = new Command<TodoItem>(DeleteItem);
            TodoItemStore.Items.CollectionChanged += OnItemsCollectionChanged;
            foreach (var item in TodoItemStore.Items)
            {
                item.PropertyChanged += OnItemPropertyChanged;
            }

            RefreshVisibleItems();
        }

        private void RefreshVisibleItems()
        {
            VisibleItems.Clear();
            foreach (var item in TodoItemStore.Items)
            {
                if (!HideCompleted || !item.IsDone)
                {
                    VisibleItems.Add(item);
                }
            }
        }

        private void OnItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems is not null)
            {
                foreach (TodoItem item in e.OldItems)
                {
                    item.PropertyChanged -= OnItemPropertyChanged;
                }
            }

            if (e.NewItems is not null)
            {
                foreach (TodoItem item in e.NewItems)
                {
                    item.PropertyChanged += OnItemPropertyChanged;
                }
            }

            RefreshVisibleItems();
        }

        private void OnItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TodoItem.IsDone))
            {
                RefreshVisibleItems();
            }
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
