using TaskManager.ViewModels;

namespace TaskManager.Pages;

/// <summary>
/// Shows details of the selected project
/// </summary>
public partial class ProjectDetailsPage : ContentPage
{
    public ProjectDetailsPage(ProjectDetailsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ProjectDetailsViewModel vm)
        {
            await vm.RefreshDataAsync();
        }
    }
}