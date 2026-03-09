using TaskManager.Services;
using TaskManager.UIModels.ProjectUIModels;
using TaskManager.UIModels.TaskUIModels;

namespace TaskManager.Pages;

/// <summary>
/// Shows details of the selected project
/// </summary>
[QueryProperty(nameof(CurrentProject), nameof(CurrentProject))]
public partial class ProjectDetailsPage : ContentPage
{
    private readonly IStorageService _storage;
    private ProjectDisplayModel _currentProject;

    /// <summary>
    /// Gets or sets the current project
    /// </summary>
    public ProjectDisplayModel CurrentProject
    {
        get => _currentProject;
        set
        {
            _currentProject = value;
            _currentProject.LoadTasks();  
            BindingContext = CurrentProject;
        }
    }

    /// <summary>
    /// Creates the page for project details
    /// </summary>
    /// <param name="storage">The service used to get data</param>
    public ProjectDetailsPage(IStorageService storage)  
    {
        InitializeComponent();
        _storage = storage;
    }

    /// <summary>
    /// Opens the selected task page
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">Data about the selection change</param>
    private void TaskSelected(object sender, SelectionChangedEventArgs e)
    {
        var task = (TaskDisplayModel)e.CurrentSelection[0];
        Shell.Current.GoToAsync($"{nameof(TaskDetailsPage)}", new Dictionary<string, object> { { nameof(TaskDetailsPage.CurrentTask), task } });
    }
}