namespace TodoListMobile
{
    public partial class CreateTodoPage : ContentPage
    {
        public CreateTodoPage()
        {
            InitializeComponent();
            SizeChanged += OnPageSizeChanged;
            UpdateVisualState();
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

            await Shell.Current.GoToAsync("..", true, new Dictionary<string, object>
            {
                ["Title"] = viewModel.Title,
                ["Description"] = viewModel.Description,
                ["DueDate"] = viewModel.DueDate
            });

            viewModel.Reset();
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
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
