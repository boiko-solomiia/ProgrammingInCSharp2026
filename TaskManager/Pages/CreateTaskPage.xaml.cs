using TaskManager.ViewModels;

namespace TaskManager.Pages;

public partial class CreateTaskPage : ContentPage
{
    public CreateTaskPage(CreateTaskViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}