using TaskManager.DBModels;
using System.Text.Json;

namespace TaskManager.Storage
{
    /// <summary>
    /// File-based implementation of the storage context
    /// Stores projects as JSON files and tasks inside project subdirectories
    /// </summary>
    public class FileStorageContext : IStorageContext
    {
        private static readonly string DatabasePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "TaskManagerData"
        );

        private readonly SemaphoreSlim _semaphore = new(1, 1);

        /// <summary>
        /// Ensures the storage directory exists and initializes with mock data if first run
        /// </summary>
        private async Task EnsureStorageReady()
        {
            await _semaphore.WaitAsync();

            try
            {
                if (!Directory.Exists(DatabasePath))
                    await InitializeStorageWithMockDataAsync();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        /// <summary>
        /// Creates initial storage structure and populates with sample data from InMemoryStorageContext
        /// </summary>
        private async Task InitializeStorageWithMockDataAsync()
        {
            Directory.CreateDirectory(DatabasePath);

            var memoryContext = new InMemoryStorageContext();
            var writeTasks = new List<Task>();

            await foreach (var project in memoryContext.GetAllProjectsAsync())
            {
                var projectFolder = GetProjectFolderPath(project.Id);
                Directory.CreateDirectory(projectFolder);

                writeTasks.Add(File.WriteAllTextAsync(
                    GetProjectFilePath(project.Id),
                    JsonSerializer.Serialize(project)));

                var projectTasks = await memoryContext.GetTasksForProjectAsync(project.Id);
                foreach (var task in projectTasks)
                {
                    writeTasks.Add(File.WriteAllTextAsync(
                        GetTaskFilePath(project.Id, task.Id),
                        JsonSerializer.Serialize(task)));
                }
            }

            await Task.WhenAll(writeTasks);
        }

        /// <summary>
        /// Returns the file path for a project's JSON file
        /// </summary>
        private string GetProjectFilePath(Guid projectId)
        {
            return Path.Combine(DatabasePath, $"{projectId}.json");
        }

        /// <summary>
        /// Returns the folder path where a project's tasks are stored
        /// </summary>
        private string GetProjectFolderPath(Guid projectId)
        {
            return Path.Combine(DatabasePath, projectId.ToString());
        }

        /// <summary>
        /// Returns the file path for a task's JSON file inside its project folder
        /// </summary>
        private string GetTaskFilePath(Guid projectId, Guid taskId)
        {
            return Path.Combine(GetProjectFolderPath(projectId), $"{taskId}.json");
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<ProjectDBModel> GetAllProjectsAsync()
        {
            await EnsureStorageReady();

            var projectFiles = Directory.GetFiles(DatabasePath, "*.json");
            foreach (var file in projectFiles)
            {
                var json = await File.ReadAllTextAsync(file);
                var project = JsonSerializer.Deserialize<ProjectDBModel>(json);
                if (project != null)
                {
                    yield return project;
                }
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TaskDBModel>> GetTasksForProjectAsync(Guid projectId)
        {
            await EnsureStorageReady();
            var tasks = new List<TaskDBModel>();

            var projectFolder = GetProjectFolderPath(projectId);
            if (!Directory.Exists(projectFolder))
            {
                return tasks;
            }

            foreach (var file in Directory.GetFiles(projectFolder, "*.json"))
            {
                var json = await File.ReadAllTextAsync(file);
                var task = JsonSerializer.Deserialize<TaskDBModel>(json);
                if (task != null)
                {
                    tasks.Add(task);
                }
            }

            return tasks;
        }

        /// <inheritdoc />
        public async Task<ProjectDBModel?> GetProjectAsync(Guid projectId)
        {
            await EnsureStorageReady();

            var filePath = GetProjectFilePath(projectId);
            if (!File.Exists(filePath))
            {
                return null;
            }

            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<ProjectDBModel>(json);
        }

        /// <inheritdoc />
        public async Task<TaskDBModel?> GetTaskAsync(Guid taskId)
        {
            await EnsureStorageReady();

            foreach (var projectFolder in Directory.GetDirectories(DatabasePath))
            {
                var taskPath = Path.Combine(projectFolder, $"{taskId}.json");
                if (File.Exists(taskPath))
                {
                    var json = await File.ReadAllTextAsync(taskPath);
                    return JsonSerializer.Deserialize<TaskDBModel>(json);
                }
            }

            return null;
        }

        /// <inheritdoc />
        public async Task<int> GetTasksCountForProjectAsync(Guid projectId)
        {
            await EnsureStorageReady();

            var projectFolder = GetProjectFolderPath(projectId);
            return Directory.Exists(projectFolder) ? Directory.GetFiles(projectFolder, "*.json").Length : 0;
        }

        /// <inheritdoc />
        public async Task<int> GetCompletedTasksCountForProjectAsync(Guid projectId)
        {
            await EnsureStorageReady();

            var projectFolder = GetProjectFolderPath(projectId);
            if (!Directory.Exists(projectFolder))
            {
                return 0;
            }

            var count = 0;
            foreach (var file in Directory.GetFiles(projectFolder, "*.json"))
            {
                var json = await File.ReadAllTextAsync(file);
                var task = JsonSerializer.Deserialize<TaskDBModel>(json);
                if (task?.IsCompleted == true)
                    count++;
            }

            return count;
        }

        /// <inheritdoc />
        public async Task AddProjectAsync(ProjectDBModel project)
        {
            await EnsureStorageReady();

            var folderPath = GetProjectFolderPath(project.Id);
            Directory.CreateDirectory(folderPath);

            var filePath = GetProjectFilePath(project.Id);
            await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(project));
        }

        /// <inheritdoc />
        public async Task UpdateProjectAsync(ProjectDBModel project)
        {
            await EnsureStorageReady();

            var filePath = GetProjectFilePath(project.Id);
            if (File.Exists(filePath))
                await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(project));
        }

        /// <inheritdoc />
        public async Task AddTaskAsync(TaskDBModel task)
        {
            await EnsureStorageReady();

            var projectFolder = GetProjectFolderPath(task.ProjectId);
            Directory.CreateDirectory(projectFolder);

            var filePath = GetTaskFilePath(task.ProjectId, task.Id);
            await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(task));
        }

        /// <inheritdoc />
        public async Task UpdateTaskAsync(TaskDBModel task)
        {
            await EnsureStorageReady();

            var filePath = GetTaskFilePath(task.ProjectId, task.Id);
            if (File.Exists(filePath))
                await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(task));
        }

        /// <inheritdoc />
        public async Task DeleteProjectAsync(Guid projectId)
        {
            await EnsureStorageReady();
            
            var folderPath = GetProjectFolderPath(projectId);
            if (Directory.Exists(folderPath))
                Directory.Delete(folderPath, recursive: true);
            
            var filePath = GetProjectFilePath(projectId);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        /// <inheritdoc />
        public async Task DeleteTaskAsync(Guid taskId)
        {
            await EnsureStorageReady();

            foreach (var projectFolder in Directory.GetDirectories(DatabasePath))
            {
                var taskPath = Path.Combine(projectFolder, $"{taskId}.json");
                if (File.Exists(taskPath))
                {
                    File.Delete(taskPath);
                    return;
                }
            }
        }
    }
}