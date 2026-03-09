namespace TaskManager
{
    /// <summary>
    /// Represents the main app
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Creates the app
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates the main window of the app
        /// </summary>
        /// <param name="activationState">Information about app activation</param>
        /// <returns>The main app window</returns>
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}