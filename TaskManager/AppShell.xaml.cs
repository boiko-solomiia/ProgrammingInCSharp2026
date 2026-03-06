using TaskManager.Pages;

namespace TaskManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute($"{nameof(ProjectsPage)}/{nameof(ProjectDetailsPage)}", typeof(ProjectDetailsPage));
            Routing.RegisterRoute($"{nameof(ProjectsPage)}/{nameof(ProjectDetailsPage)}/{nameof(TaskDetailsPage)}", typeof(TaskDetailsPage));
        }
    }
}
