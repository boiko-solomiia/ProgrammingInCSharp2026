using TaskManager.ViewModels;

namespace TaskManager.Pages;

/// <summary>
/// Allows to create new project
/// </summary>
public partial class CreateProjectPage : ContentPage
{
    public CreateProjectPage(CreateProjectViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}