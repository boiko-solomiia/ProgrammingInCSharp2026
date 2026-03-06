using System.Collections.ObjectModel;
using TaskManager.Services;
using TaskManager.UIModels.ProjectUIModels;

namespace TaskManager.Pages;

public partial class ProjectsPage : ContentPage
{
    private IStorageService _storage;
    
    public ObservableCollection<ProjectDisplayModel> Projects { get; set; }
    
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

    private async void ProjectSelected(object sender, SelectionChangedEventArgs e)
    {
        var project = (ProjectDisplayModel)e.CurrentSelection[0];
        await Shell.Current.GoToAsync($"{nameof(ProjectDetailsPage)}", new Dictionary<string, object> { { nameof(ProjectDetailsPage.CurrentProject), project } });
    }
}