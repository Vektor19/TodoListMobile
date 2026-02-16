# TodoListMobile

A modern cross-platform Todo List application built with .NET MAUI, featuring local data persistence, weather integration, and image attachments.

## Features

- ✅ **Task Management**: Create, edit, delete, and mark tasks as complete
- 📅 **Due Date Tracking**: Set and track due dates for your tasks
- 📷 **Image Attachments**: Add photos to your tasks
- 🌤️ **Weather Integration**: Real-time weather display for your location
- 💾 **Local Storage**: SQLite database for reliable data persistence
- 📝 **Activity Logging**: Automatic logging of all task operations
- 🎨 **Modern UI**: Clean and intuitive MAUI interface
- 🔍 **Task Filtering**: Hide/show completed tasks

## Technologies

- **.NET 9**: Latest .NET framework
- **.NET MAUI**: Cross-platform UI framework
- **SQLite**: Local database for data persistence
- **MVVM Pattern**: Clean architecture with ViewModel separation
- **Open-Meteo API**: Weather data integration

## Architecture

The application follows the MVVM (Model-View-ViewModel) pattern:

- **Models**: Data entities (`TodoItem`, `WeatherData`)
- **ViewModels**: Business logic and UI state management
- **Views**: XAML pages for user interface
- **Services**: Data access and external API communication

## Project Structure

```
TodoListMobile/
├── Models/                    # Data models
│   ├── TodoItem.cs           # Todo item entity
│   ├── TodoItemStore.cs      # In-memory store
│   └── WeatherData.cs        # Weather data model
├── ViewModels/               # ViewModels
│   ├── BaseViewModel.cs      # Base ViewModel class
│   ├── TodoListPageViewModel.cs
│   └── CreateTodoViewModel.cs
├── Services/                 # Business services
│   ├── TodoStorageService.cs # SQLite data access
│   └── WeatherService.cs     # Weather API client
├── Validators/               # Input validation
│   └── TaskValidator.cs      # Task validation logic
├── Resources/                # App resources
│   └── Strings/             # Localization
└── Pages/                    # XAML views
    ├── TodoListPage.xaml
    └── CreateTodoPage.xaml
```

## Getting Started

### Prerequisites

- Visual Studio 2022 (17.8 or later)
- .NET 9 SDK
- MAUI workload installed

### Installation

1. Clone the repository:
```bash
git clone https://github.com/Vektor19/TodoListMobile.git
cd TodoListMobile
```n
2. Restore NuGet packages:
```bash
dotnet restore
```n
3. Build the solution:
```bash
dotnet build
```n
4. Run the application:
```bash
dotnet run
```n
### Running on Specific Platforms

**Android:**
```bash
dotnet build -t:Run -f net9.0-android
```n
**iOS:**
```bash
dotnet build -t:Run -f net9.0-ios
```n
**Windows:**
```bash
dotnet build -t:Run -f net9.0-windows
```n
**macOS:**
```bash
dotnet build -t:Run -f net9.0-maccatalyst
```n
## Usage

### Creating a Task

1. Tap the "+" button or "Add Task" button
2. Enter task title and description
3. Select a due date
4. Optionally, add a photo by tapping "Add Photo"
5. Tap "Save" to create the task

### Managing Tasks

- **Mark as Complete**: Tap the checkbox next to a task
- **Edit Task**: Tap the edit icon (✏️) on a task
- **Delete Task**: Tap the delete icon (🗑️) on a task
- **Filter Tasks**: Use the "Hide Completed" toggle to show/hide completed tasks

### Weather Display

The current weather for your location (default: Kyiv) is displayed at the top of the main screen, automatically refreshed when the app launches.

## Data Storage

- **Database**: SQLite database (`todo.db3`) stored in app data directory
- **Logs**: Activity log file (`todo_notes.txt`) for audit trail
- **Images**: Photos stored in app data directory with unique filenames

## Configuration

### Changing Default City

To change the default weather location, modify the `LoadWeatherAsync` method in `TodoListPageViewModel.cs`:

```csharp
var weather = await _weatherService.GetWeatherAsync("YourCity");
```n
Supported cities: Kyiv, Lviv, Odesa (or custom coordinates can be added to `WeatherService.cs`)

## Dependencies

- **sqlite-net-pcl**: SQLite ORM for .NET
- **Open-Meteo API**: Free weather API (no API key required)

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is open source and available under the [MIT License](LICENSE).

## Author

- **GitHub**: [@Vektor19](https://github.com/Vektor19)

## Acknowledgments

- Weather data provided by [Open-Meteo](https://open-meteo.com/)
- Built with [.NET MAUI](https://dotnet.microsoft.com/apps/maui)

## Support

For issues, questions, or suggestions, please open an issue in the GitHub repository.

---

**Note**: This application is designed for educational purposes and personal use. Feel free to customize and extend it to meet your specific needs.
