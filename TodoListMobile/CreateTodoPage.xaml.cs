namespace TodoListMobile
{
    public partial class CreateTodoPage : ContentPage, IQueryAttributable
    {
        public CreateTodoPage()
        {
            InitializeComponent();
            SizeChanged += OnPageSizeChanged;
            UpdateVisualState();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (BindingContext is not ViewModels.CreateTodoViewModel viewModel)
            {
                return;
            }

            if (query.TryGetValue("TodoItem", out var item) && item is Models.TodoItem todoItem)
            {
                viewModel.BeginEdit(todoItem);
            }
        }

        private async void OnAddTaskClicked(object sender, EventArgs e)
        {
            if (BindingContext is not ViewModels.CreateTodoViewModel viewModel)
            {
                return;
            }

            var confirmation = await DisplayAlert(
                "Confirm Task",
                $"Do you really want to add this task?\n\n" +
                $"Title: {viewModel.Title}\n" +
                $"Description: {viewModel.Description}\n" +
                $"Due Date: {viewModel.DueDate:d}",
                "Yes",
                "No");

            if (!confirmation)
            {
                return;
            }

            viewModel.Save();
            await Shell.Current.GoToAsync("..", true);
            viewModel.Reset();
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            if (BindingContext is ViewModels.CreateTodoViewModel viewModel)
            {
                viewModel.Reset();
            }

            await Shell.Current.GoToAsync("..");
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
