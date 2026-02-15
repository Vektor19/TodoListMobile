namespace TodoListMobile
{
    public partial class TodoListPage : ContentPage, IQueryAttributable
    {
        public TodoListPage()
        {
            InitializeComponent();
            SizeChanged += OnPageSizeChanged;
            UpdateVisualState();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (BindingContext is not ViewModels.TodoListPageViewModel viewModel)
            {
                return;
            }

            if (query.TryGetValue("Title", out var title) && title is string titleValue)
            {
                viewModel.Title = titleValue;
            }

            if (query.TryGetValue("Description", out var description) && description is string descriptionValue)
            {
                viewModel.Description = descriptionValue;
            }

            if (query.TryGetValue("DueDate", out var dueDate) && dueDate is DateTime dueDateValue)
            {
                viewModel.DueDate = dueDateValue;
            }
        }

        private async void OnAddNewClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CreateTodoPage));
        }

        private void OnPageSizeChanged(object sender, EventArgs e)
        {
            UpdateVisualState();
        }

        private void UpdateVisualState()
        {
            double width = Width;
            double height = Height;

            if (width <= 0 || height <= 0)
            {
                return;
            }

            string sizeState;
            if (width >= 1200)
                sizeState = "ExtraLargeScreen";
            else if (width >= 900)
                sizeState = "LargeScreen";
            else if (width >= 600)
                sizeState = "MediumScreen";
            else
                sizeState = "SmallScreen";

            string orientationState = width > height ? "Landscape" : "Portrait";

            VisualStateManager.GoToState(MainLayout, sizeState);
            VisualStateManager.GoToState(MainLayout, orientationState);

            if (orientationState == "Landscape")
            {
                FormContainer.WidthRequest = width * 0.65;
            }
            else
            {
                FormContainer.WidthRequest = -1;
            }
        }
    }
}
