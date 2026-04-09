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
        public IEnumerable<TaskDBModel> GetTasksForProject(Guid projectId)
        {
            return _storageContext.GetTasksForProject(projectId);
        }

        /// <inheritdoc />
        public TaskDBModel GetTask(Guid projectId, Guid taskId)
        {
            var tasks = _storageContext.GetTasksForProject(projectId);
            var task = tasks.FirstOrDefault(t => t.Id == taskId);
            return task;
        }

        /// <inheritdoc />
        public int GetTasksCountForProject(Guid projectId)
        {
            return _storageContext.GetTasksCountForProject(projectId);
        }

        /// <inheritdoc />
        public int GetCompletedTasksCountForProject(Guid projectId)
        {
            return _storageContext.GetCompletedTasksCountForProject(projectId);
        }

        /// <inheritdoc />
        public void AddTask(TaskDBModel task)
        {
            _storageContext.AddTask(task);
        }

        /// <inheritdoc />
        public void UpdateTask(TaskDBModel task)
        {
            _storageContext.UpdateTask(task);
        }

        public void DeleteTask(Guid taskId)
        {
            _storageContext.DeleteTask(taskId);
        }
    }
}