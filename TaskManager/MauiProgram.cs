using Microsoft.Extensions.Logging;
using TaskManager.Pages;
using TaskManager.Services;

namespace TaskManager
{
    public static class MauiProgram
    {
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
            builder.Services.AddSingleton<IStorageService, StorageService>();
            
            builder.Services.AddSingleton<ProjectsPage>();        
            builder.Services.AddTransient<ProjectDetailsPage>();  
            builder.Services.AddTransient<TaskDetailsPage>();

            return builder.Build();
        }
    }
}
