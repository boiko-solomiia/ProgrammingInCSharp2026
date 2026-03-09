using TaskManager.UIModels.ProjectUIModels;
using TaskManager.UIModels.TaskUIModels;

namespace TaskManager.Pages;

/// <summary>
/// Shows details of the selected task
/// </summary>
[QueryProperty(nameof(CurrentTask), nameof(CurrentTask))]
public partial class TaskDetailsPage : ContentPage
{
    private TaskDisplayModel _currentTask;

    /// <summary>
    /// Gets or sets the current task
    /// </summary>
    public TaskDisplayModel CurrentTask
    {
        get => _currentTask;
        set
        {
            _currentTask = value;
            BindingContext = CurrentTask;
        }
    }

    /// <summary>
    /// Creates the task details page
    /// </summary>
    public TaskDetailsPage()
	{
		InitializeComponent();
	}
}