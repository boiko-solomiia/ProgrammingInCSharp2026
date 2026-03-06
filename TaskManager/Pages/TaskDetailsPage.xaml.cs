using TaskManager.UIModels.ProjectUIModels;
using TaskManager.UIModels.TaskUIModels;

namespace TaskManager.Pages;

[QueryProperty(nameof(CurrentTask), nameof(CurrentTask))]
public partial class TaskDetailsPage : ContentPage
{
    private TaskDisplayModel _currentTask;

    public TaskDisplayModel CurrentTask
    {
        get => _currentTask;
        set
        {
            _currentTask = value;
            BindingContext = CurrentTask;
        }
    }
    public TaskDetailsPage()
	{
		InitializeComponent();
	}
}