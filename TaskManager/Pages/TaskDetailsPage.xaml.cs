using TaskManager.ViewModels;

namespace TaskManager.Pages;

/// <summary>
/// Shows details of the selected task
/// </summary>
public partial class TaskDetailsPage : ContentPage
{
    /// <summary>
    /// Creates the task details page and assigns the view model
    /// </summary>
    /// <param name="vm">The view model for the task details page</param>
    public TaskDetailsPage(TaskDetailsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}