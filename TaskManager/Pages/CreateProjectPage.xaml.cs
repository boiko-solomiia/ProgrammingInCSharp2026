using TaskManager.ViewModels;

namespace TaskManager.Pages;
public partial class CreateProjectPage : ContentPage
{
    public CreateProjectPage(CreateProjectViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}