using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Services;
using TaskManager.UIModels.ProjectUIModels;

namespace TaskManager.Pages;

public partial class ProjectsPage : ContentPage
{
    private StorageService _storage;
    
    public ObservableCollection<ProjectDisplayModel> Projects { get; set; }
    
    public ProjectsPage()
    {
        InitializeComponent();
        _storage = new StorageService();
        Projects = new ObservableCollection<ProjectDisplayModel>();
        foreach (var project in _storage.GetAllProjects())
        {
            var model = new ProjectDisplayModel(project);
            model.LoadTasks(_storage);
            Projects.Add(model);
        } 
        BindingContext = this;
    }
    
    private async void ProjectSelected(object sender, SelectionChangedEventArgs e)
    {
        var project = (ProjectDisplayModel)e.CurrentSelection[0];
        //await Shell.Current.GoToAsync($"{nameof(DepartmentDetailsPage)}", new Dictionary<string, object> { { nameof(DepartmentDetailsPage.CurrentDepartment), department} });
    }
}