using TaskManager.DBModels;
using TaskManager.Storage;

namespace TaskManager.Repositories
{
    /// <summary>
    /// Repository implementation for task data access.
    /// Provides methods for retrieving individual tasks and task statistics.
    /// Acts as an intermediary between the service layer and the storage context
    /// </summary>
    public class TaskRepository : ITaskRepository
    {
        private readonly IStorageContext _storageContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskRepository"/> class.
        /// </summary>
        /// <param name="storageContext">The storage context providing access to the underlying data store</param>
        public TaskRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        /// <inheritdoc />
        public Task<IEnumerable<TaskDBModel>> GetTasksForProjectAsync(Guid projectId)
        {
            return _storageContext.GetTasksForProjectAsync(projectId);
        }

        /// <inheritdoc />
        public Task<TaskDBModel?> GetTaskAsync(Guid taskId)
        {
            return _storageContext.GetTaskAsync(taskId);
        }

        /// <inheritdoc />
        public Task<int> GetTasksCountForProjectAsync(Guid projectId)
        {
            return _storageContext.GetTasksCountForProjectAsync(projectId);
        }

        /// <inheritdoc />
        public Task<int> GetCompletedTasksCountForProjectAsync(Guid projectId)
        {
            return _storageContext.GetCompletedTasksCountForProjectAsync(projectId);
        }

        /// <inheritdoc />
        public Task AddTaskAsync(TaskDBModel task)
        {
            return _storageContext.AddTaskAsync(task);
        }

        /// <inheritdoc />
        public Task UpdateTaskAsync(TaskDBModel task)
        {
            return _storageContext.UpdateTaskAsync(task);
        }

        /// <inheritdoc />
        public Task DeleteTaskAsync(Guid taskId)
        {
            return _storageContext.DeleteTaskAsync(taskId);
        }
    }
}