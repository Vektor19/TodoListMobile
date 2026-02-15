namespace TodoListMobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CreateTodoPage), typeof(CreateTodoPage));
        }
    }
}
