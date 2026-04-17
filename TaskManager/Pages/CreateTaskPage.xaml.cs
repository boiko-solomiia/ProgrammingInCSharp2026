using TaskManager.ViewModels;

namespace TaskManager.Pages;

/// <summary>
/// Allows to create new task
/// </summary>
public partial class CreateTaskPage : ContentPage
{
    public CreateTaskPage(CreateTaskViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}