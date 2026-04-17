using TaskManager.ViewModels;

namespace TaskManager.Pages;

/// <summary>
/// Allows to edit the selected project
/// </summary>
public partial class EditProjectPage : ContentPage
{
    public EditProjectPage(EditProjectViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
