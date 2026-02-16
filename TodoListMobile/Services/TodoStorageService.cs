using SQLite;
using TodoListMobile.Models;

namespace TodoListMobile.Services
{
    public class TodoStorageService
    {
        private const string DatabaseName = "todo.db3";
        private const string NotesFileName = "todo_notes.txt";
        private readonly SQLiteAsyncConnection _database;
        private readonly string _notesPath;

        public TodoStorageService()
        {
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, DatabaseName);
            _database = new SQLiteAsyncConnection(databasePath);
            _notesPath = Path.Combine(FileSystem.AppDataDirectory, NotesFileName);
        }

        public Task InitializeAsync()
        {
            return _database.CreateTableAsync<TodoItem>();
        }

        public Task<List<TodoItem>> GetItemsAsync()
        {
            return _database.Table<TodoItem>().ToListAsync();
        }

        public Task<int> SaveItemAsync(TodoItem item)
        {
            return item.Id == 0 ? _database.InsertAsync(item) : _database.UpdateAsync(item);
        }

        public Task<int> DeleteItemAsync(TodoItem item)
        {
            return _database.DeleteAsync(item);
        }

        public Task<int> ClearAsync()
        {
            return _database.DeleteAllAsync<TodoItem>();
        }

        public async Task<bool> HasAnyAsync()
        {
            return await _database.Table<TodoItem>().CountAsync() > 0;
        }

        public Task SaveNoteAsync(TodoItem item)
        {
            var line = $"{DateTime.UtcNow:O} | {item.Title} | {item.Description} | {item.DueDate:d} | Done: {item.IsDone}";
            return File.AppendAllTextAsync(_notesPath, line + Environment.NewLine);
        }
    }
}
