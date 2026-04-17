using TaskManager.ViewModels;

namespace TaskManager.Pages;

/// <summary>
/// Shows the list of all projects
/// </summary>
public partial class ProjectsPage : ContentPage
{
    public ProjectsPage(ProjectsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ProjectsViewModel vm)
        {
            await vm.RefreshDataAsync();
        }
    }
}