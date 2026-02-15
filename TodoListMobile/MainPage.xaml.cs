namespace TodoListMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            SizeChanged += OnPageSizeChanged;
            UpdateVisualState();
        }

        private void OnPageSizeChanged(object sender, EventArgs e)
        {
            UpdateVisualState();
        }

        private void UpdateVisualState()
        {
            double width = this.Width;
            double height = this.Height;
            
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
