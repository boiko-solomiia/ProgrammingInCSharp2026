using TaskManager.ViewModels;

namespace TaskManager.Pages;

public partial class EditProjectPage : ContentPage
{
    public EditProjectPage(EditProjectViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
