using TaskManager.ViewModels;

namespace TaskManager.Pages;

/// <summary>
/// Shows details of the selected project
/// </summary>
public partial class ProjectDetailsPage : ContentPage
{
    /// <summary>
    /// Creates the page and assigns the view model
    /// </summary>
    /// <param name="vm">The view model for the project details page</param>
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