using CommunityToolkit.Mvvm.Input;
using TaskManager.Common.Enums;
using TaskManager.DTOModels.ProjectDTO;
using TaskManager.Services;

namespace TaskManager.ViewModels
{
    public partial class CreateProjectViewModel : BaseViewModel
    {
        private readonly IProjectService _projectService;

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

        public CreateProjectViewModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [RelayCommand]
        private async Task Create()
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
                var dto = new ProjectCreateDTO(Name.Trim(), Description.Trim(), ProjectType.Value);
                await _projectService.CreateProjectAsync(dto);
                await Shell.Current.DisplayAlertAsync("Success", "Project created successfully!", "OK");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to create project: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}