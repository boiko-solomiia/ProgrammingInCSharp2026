using TaskManager.Services;
using TaskManager.UIModels.ProjectUIModels;
using TaskManager.UIModels.TaskUIModels;

namespace TaskManager.Pages;

[QueryProperty(nameof(CurrentProject), nameof(CurrentProject))]
public partial class ProjectDetailsPage : ContentPage
{
    private readonly IStorageService _storage;
    private ProjectDisplayModel _currentProject;

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
    
    public ProjectDetailsPage(IStorageService storage)  
    {
        InitializeComponent();
        _storage = storage;
    }

    private void TaskSelected(object sender, SelectionChangedEventArgs e)
    {
        var task = (TaskDisplayModel)e.CurrentSelection[0];
        Shell.Current.GoToAsync($"{nameof(TaskDetailsPage)}", new Dictionary<string, object> { { nameof(TaskDetailsPage.CurrentTask), task } });
    }
}