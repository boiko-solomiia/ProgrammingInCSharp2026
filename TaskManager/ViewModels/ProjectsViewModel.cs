using System.Collections.ObjectModel;
using TaskManager.DTOModels.ProjectDTO;
using TaskManager.Pages;
using TaskManager.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TaskManager.ViewModels
{
    /// <summary>
    /// ViewModel for the Projects list page.
    /// Displays all projects and handles navigation to project details
    /// </summary>
    public partial class ProjectsViewModel : BaseViewModel
    {
        private readonly IProjectService _projectService;

        private ObservableCollection<ProjectListDTO> _projects = new();
        public ObservableCollection<ProjectListDTO> Projects
        {
            get => _projects;
            set => SetProperty(ref _projects, value);
        }

        private ProjectListDTO? _selectedProject;
        public ProjectListDTO? SelectedProject
        {
            get => _selectedProject;
            set => SetProperty(ref _selectedProject, value);
        }

        public ProjectsViewModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        internal async Task RefreshData()
        {
            IsBusy = true;
            try
            {
                Projects = new ObservableCollection<ProjectListDTO>(_projectService.GetAllProjects());
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to load projects: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Navigates to the selected project's details page
        /// </summary>

        [RelayCommand]
        private async Task LoadProject()
        {
            IsBusy = true;
            try
            {
                if(SelectedProject == null)
                    return;
                await Shell.Current.GoToAsync(nameof(ProjectDetailsPage), new Dictionary<string, object> { { "ProjectId", SelectedProject.Id } });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to navigate to project details: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task AddProject()
        {
            IsBusy = true;
            try
            {
                await Shell.Current.GoToAsync(nameof(CreateProjectPage));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to navigate to create project page: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task EditProject(ProjectListDTO? project)
        {
            if (project == null)
                return;

            IsBusy = true;
            try
            {
                await Shell.Current.GoToAsync(nameof(EditProjectPage), new Dictionary<string, object> { { "ProjectId", project.Id } });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to navigate to edit project page: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task DeleteProject(ProjectListDTO? project)
        {
            if (project == null)
                return;

            bool confirm = await Shell.Current.DisplayAlertAsync("Delete project", $"Are you sure you want to delete project \"{project.Name}\"?", "Yes", "No");

            if (!confirm)
                return;

            IsBusy = true;
            try
            {
                _projectService.DeleteProject(project.Id);
                await RefreshData();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to delete project: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}