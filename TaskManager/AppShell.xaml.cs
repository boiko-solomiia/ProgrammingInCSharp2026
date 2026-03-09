using TaskManager.Pages;

namespace TaskManager
{
    /// <summary>
    /// Sets up page routes for the app
    /// </summary>
    public partial class AppShell : Shell
    {
        /// <summary>
        /// Creates the app shell and registers page routes
        /// </summary>
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute($"{nameof(ProjectsPage)}/{nameof(ProjectDetailsPage)}", typeof(ProjectDetailsPage));
            Routing.RegisterRoute($"{nameof(ProjectsPage)}/{nameof(ProjectDetailsPage)}/{nameof(TaskDetailsPage)}", typeof(TaskDetailsPage));
        }
    }
}
