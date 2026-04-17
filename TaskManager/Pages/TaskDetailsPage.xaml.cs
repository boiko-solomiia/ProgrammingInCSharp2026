using TaskManager.ViewModels;

namespace TaskManager.Pages;

/// <summary>
/// Shows details of the selected task
/// </summary>
public partial class TaskDetailsPage : ContentPage
{
    public TaskDetailsPage(TaskDetailsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is TaskDetailsViewModel vm)
        {
            await vm.RefreshDataAsync();
        }
    }
}