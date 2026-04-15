using Microsoft.Extensions.Logging;
using TaskManager.Pages;
using TaskManager.Repositories;
using TaskManager.Services;
using TaskManager.Storage;
using TaskManager.ViewModels;


namespace TaskManager
{
    /// <summary>
    /// Configures and creates the MAUI app
    /// </summary>
    public static class MauiProgram
    {
        /// <summary>
        /// Creates the app, registers services, and sets fonts
        /// </summary>
        /// <returns>The configured MAUI app</returns>
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<IStorageContext, FileStorageContext>();
            
            builder.Services.AddSingleton<IProjectRepository, ProjectRepository>();
            builder.Services.AddSingleton<ITaskRepository, TaskRepository>();


            builder.Services.AddSingleton<IProjectService, ProjectService>();
            builder.Services.AddSingleton<ITaskService, TaskService>();
            
            
            builder.Services.AddSingleton<ProjectsPage>();        
            builder.Services.AddTransient<ProjectDetailsPage>();  
            builder.Services.AddTransient<TaskDetailsPage>();

            builder.Services.AddSingleton<ProjectsViewModel>();
            builder.Services.AddTransient<ProjectDetailsViewModel>();
            builder.Services.AddTransient<TaskDetailsViewModel>();

            builder.Services.AddTransient<EditTaskPage>();
            builder.Services.AddTransient<EditTaskViewModel>();

            builder.Services.AddTransient<CreateTaskPage>();
            builder.Services.AddTransient<CreateTaskViewModel>();

            builder.Services.AddTransient<CreateProjectPage>();
            builder.Services.AddTransient<CreateProjectViewModel>();

            builder.Services.AddTransient<EditProjectPage>();
            builder.Services.AddTransient<EditProjectViewModel>();

            return builder.Build();
        }
    }
}
