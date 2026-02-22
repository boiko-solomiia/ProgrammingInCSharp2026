using TaskManager.Services;
using TaskManager.UIModels.ProjectUIModels;

namespace TaskManagerConsoleApp
{
    public class ConsoleApp
    {
        private static StorageService _storage;
       
        /// <summary>
        /// Application entry point. Initializes storage service and runs main interaction loop
        /// </summary>
        public static void Main()
        {
            _storage = new StorageService();
            Console.WriteLine("TASK MANAGER CONSOLE APP");

            while (true)
            {
                PrintMenu();
                int choice = ReadInt("Choose option: ");
                if (choice == 0)
                {
                    break;
                }
                else if (choice == 1)
                {
                    ShowProjects();
                }
                else if (choice == 2)
                {
                    ShowProjectTasks();
                }
                else
                {
                    Console.WriteLine("Unknown option");
                }

                Console.WriteLine("\nPress Enter to continue");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Displays main menu options
        /// </summary>
        private static void PrintMenu()
        {
            Console.WriteLine("MENU:");
            Console.WriteLine("0 - Exit");
            Console.WriteLine("1 - Show all projects");
            Console.WriteLine("2 - Show tasks of a project");
        }


        /// <summary>
        /// Retrieves all projects from storage and converts them to display models.
        /// Loads associated tasks for each project
        /// </summary>
        /// <returns>Collection of project display models with loaded tasks</returns>
        private static List<ProjectDisplayModel> GetProjectDisplayModels()
        {
            var projects = new List<ProjectDisplayModel>();
            foreach (var p in _storage.GetAllProjects())
            {
                var model = new ProjectDisplayModel(p);
                model.LoadTasks(_storage);
                projects.Add(model);
            }
            return projects;
        }

        /// <summary>
        /// Displays list of all projects with their progress information
        /// </summary>
        private static void ShowProjects()
        {
            var projects = GetProjectDisplayModels();
            if (projects.Count == 0)
            {
                Console.WriteLine("No projects");
            }
            else
            {
                Console.WriteLine("Projects:");
                for (int i = 0; i < projects.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {projects[i]}");
                }
            }
        }

        /// <summary>
        /// Handles project selection and displays its tasks
        /// </summary>
        private static void ShowProjectTasks()
        {
            var projects = GetProjectDisplayModels();
            if (projects.Count == 0)
            {
                Console.WriteLine("No projects");
            }
            else
            {
                Console.WriteLine("Choose project:");
                for (int i = 0; i < projects.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {projects[i].Name}");
                }
                int index = ReadInt("Enter project number: ") - 1;
                if (index < 0 || index >= projects.Count)
                {
                    Console.WriteLine("Invalid choice");
                    return;
                }
                var project = projects[index];
                project.LoadTasks(_storage);
                if (project.Tasks.Count == 0)
                {
                    Console.WriteLine("No tasks");
                }
                else
                {
                    Console.WriteLine($"\nTasks in project '{project.Name}':");
                    foreach (var task in project.Tasks)
                    {
                        Console.WriteLine($"- {task}");
                    }
                }
            }
        }

        /// <summary>
        /// Reads integer from console with validation
        /// (keeps asking until valid number is entered)
        /// </summary>
        /// <param name="str">Prompt message</param>
        /// <returns>Valid integer input</returns>
        private static int ReadInt(string str)
        {
            while (true)
            {
                Console.Write(str);
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int choice))
                    return choice;
                Console.WriteLine("Must be a number. Choose again: ");
            }
        }
    }
}