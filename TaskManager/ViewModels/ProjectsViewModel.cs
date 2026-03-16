using System.Collections.ObjectModel;
using TaskManager.DTOModels.ProjectDTO;
using TaskManager.Pages;
using TaskManager.Services;

namespace TaskManager.ViewModels
{
    public class ProjectsViewModel
    {
        private readonly IProjectService _projectService;

        public ObservableCollection<ProjectListDTO> Projects { get; set; }
        public ProjectListDTO? SelectedProject { get; set; }
        public Command ProjectSelectedCommand { get; }

        public ProjectsViewModel(IProjectService projectService)
        {
            _projectService = projectService;

            Projects = new ObservableCollection<ProjectListDTO>(_projectService.GetAllProjects());
            ProjectSelectedCommand = new Command(LoadProject);
        }

        private void LoadProject()
        {
            if (SelectedProject == null)
                return;

            Shell.Current.GoToAsync($"{nameof(ProjectDetailsPage)}", new Dictionary<string, object>{ { "ProjectId", SelectedProject.Id }});
        }
    }
}