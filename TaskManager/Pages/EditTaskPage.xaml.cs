using TaskManager.ViewModels;

namespace TaskManager.Pages;

/// <summary>
/// Allows to edit the selected task
/// </summary>
public partial class EditTaskPage : ContentPage
{
    public EditTaskPage(EditTaskViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}