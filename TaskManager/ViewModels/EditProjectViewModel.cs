using CommunityToolkit.Mvvm.Input;
using TaskManager.Common.Enums;
using TaskManager.DTOModels.ProjectDTO;
using TaskManager.Services;

namespace TaskManager.ViewModels
{
    public partial class EditProjectViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IProjectService _projectService;

        private Guid _projectId;

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private ProjectType? _projectType;
        public ProjectType? ProjectType
        {
            get => _projectType;
            set => SetProperty(ref _projectType, value);
        }

        public Array ProjectTypes
        {
            get => Enum.GetValues(typeof(ProjectType));
        }

        public EditProjectViewModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("ProjectId", out var value) && value is Guid id)
                _projectId = id;

            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            IsBusy = true;
            try
            {
                var project = await _projectService.GetProjectForEditAsync(_projectId);
                if (project == null)
                {
                    await Shell.Current.DisplayAlertAsync("Error", "Project not found.", "OK");
                    await Shell.Current.GoToAsync("..");
                    return;
                }

                Name = project.Name;
                Description = project.Description;
                ProjectType = project.ProjectType;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to load project: {ex.Message}", "OK");
                await Shell.Current.GoToAsync("..");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task Save()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Shell.Current.DisplayAlertAsync("Validation error", "Project name is required.", "OK");
                return;
            }

            if (ProjectType == null)
            {
                await Shell.Current.DisplayAlertAsync("Validation error", "Project type is required.", "OK");
                return;
            }

            IsBusy = true;
            try
            {
                var dto = new ProjectEditDTO(_projectId, Name.Trim(), Description.Trim(), ProjectType.Value);
                await _projectService.UpdateProjectAsync(dto);
                await Shell.Current.DisplayAlertAsync("Success", "Project updated successfully!", "OK");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to update project: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}