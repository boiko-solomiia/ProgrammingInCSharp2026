using System.Collections.ObjectModel;
using TaskManager.Services;
using TaskManager.UIModels.ProjectUIModels;

namespace TaskManager.Pages;

/// <summary>
/// Shows the list of all projects
/// </summary>
public partial class ProjectsPage : ContentPage
{
    private IStorageService _storage;

    /// <summary>
    /// Gets the collection of projects for display
    /// </summary>
    public ObservableCollection<ProjectDisplayModel> Projects { get; set; }

    /// <summary>
    /// Creates the page and loads all projects
    /// </summary>
    /// <param name="storageService">The service used to get projects and tasks</param>
    public ProjectsPage(IStorageService storageService)
    {
        InitializeComponent();
        _storage = storageService;
        Projects = new ObservableCollection<ProjectDisplayModel>();
        
        foreach (var project in _storage.GetAllProjects())
        {
            var model = new ProjectDisplayModel(_storage, project);
            model.LoadTasks();
            Projects.Add(model);
        } 

        BindingContext = this;
    }

    /// <summary>
    /// Opens the selected project page
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">Data about the selection change</param>
    private async void ProjectSelected(object sender, SelectionChangedEventArgs e)
    {
        var project = (ProjectDisplayModel)e.CurrentSelection[0];
        await Shell.Current.GoToAsync($"{nameof(ProjectDetailsPage)}", new Dictionary<string, object> { { nameof(ProjectDetailsPage.CurrentProject), project } });
    }
}