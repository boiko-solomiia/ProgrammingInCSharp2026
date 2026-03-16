using TaskManager.ViewModels;

namespace TaskManager.Pages;

/// <summary>
/// Shows the list of all projects
/// </summary>
public partial class ProjectsPage : ContentPage
{
    /// <summary>
    /// Creates the page and assigns the view model
    /// </summary>
    /// <param name="vm">The view model for the projects page</param>
    public ProjectsPage(ProjectsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}