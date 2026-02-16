using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TodoListMobile.Services;

namespace TodoListMobile.Models
{
    public static class TodoItemStore
    {
        private static readonly TodoStorageService Storage = new();
        private static bool _initialized;

        public static ObservableCollection<TodoItem> Items { get; } = new();

        public static async Task InitializeAsync()
        {
            if (_initialized)
            {
                return;
            }

            await Storage.InitializeAsync();
            var items = await Storage.GetItemsAsync();

            if (items.Count == 0)
            {
                var seededItems = new List<TodoItem>
                {
                    new()
                    {
                        Title = "Buy groceries",
                        Description = "Milk, eggs, bread",
                        DueDate = DateTime.Today.AddDays(1)
                    },
                    new()
                    {
                        Title = "Finish report",
                        Description = "Finalize the quarterly summary",
                        DueDate = DateTime.Today.AddDays(3)
                    },
                    new()
                    {
                        Title = "Call plumber",
                        Description = "Schedule a visit for the kitchen sink",
                        DueDate = DateTime.Today.AddDays(2)
                    }
                };

                foreach (var item in seededItems)
                {
                    await Storage.SaveItemAsync(item);
                    await Storage.SaveNoteAsync(item);
                }

                items = await Storage.GetItemsAsync();
            }

            Items.Clear();
            foreach (var item in items)
            {
                Items.Add(item);
            }

            _initialized = true;
        }

        public static async Task AddOrUpdateAsync(TodoItem item)
        {
            await Storage.SaveItemAsync(item);
            await Storage.SaveNoteAsync(item);

            if (!Items.Contains(item))
            {
                Items.Add(item);
            }
        }

        public static async Task DeleteAsync(TodoItem item)
        {
            await Storage.DeleteItemAsync(item);
            Items.Remove(item);
        }

        public static async Task ClearAsync()
        {
            await Storage.ClearAsync();
            Items.Clear();
        }

        public static async Task RefreshAsync()
        {
            var items = await Storage.GetItemsAsync();
            Items.Clear();
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }
    }
}
