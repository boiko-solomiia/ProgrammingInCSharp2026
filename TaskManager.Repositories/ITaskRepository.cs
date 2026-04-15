using TaskManager.DBModels;

namespace TaskManager.Repositories
{
    /// <summary>
    /// Provides data access operations for task entities.
    /// Defines the contract for retrieving task data from the storage layer.
    /// Includes methods for both individual tasks and aggregated task statistics
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>
        /// Retrieves all tasks belonging to a specific project asynchronously
        /// </summary>
        /// <param name="projectId">The unique identifier of the project</param>
        /// <returns>A collection of task database models for the specified project</returns>
        Task<IEnumerable<TaskDBModel>> GetTasksForProjectAsync(Guid projectId);

        /// <summary>
        /// Retrieves a specific task within a project by its unique identifier asynchronously
        /// </summary>
        /// <param name="taskId">The unique identifier of the task</param>
        /// <returns>The task database model if found; otherwise, null</returns>
        Task<TaskDBModel?> GetTaskAsync(Guid taskId);

        /// <summary>
        /// Gets the total number of tasks for a specific project asynchronously
        /// </summary>
        /// <param name="projectId">The unique identifier of the project</param>
        /// <returns>The count of all tasks in the project</returns>
        Task<int> GetTasksCountForProjectAsync(Guid projectId);

        /// <summary>
        /// Gets the number of completed tasks for a specific project asynchronously
        /// </summary>
        /// <param name="projectId">The unique identifier of the project</param>
        /// <returns>The count of completed tasks in the project</returns>
        Task<int> GetCompletedTasksCountForProjectAsync(Guid projectId);

        /// <summary>
        /// Adds a new task to storage asynchronously
        /// </summary>
        /// <param name="task">The task database model to add</param>
        Task AddTaskAsync(TaskDBModel task);

        /// <summary>
        /// Updates an existing task in storage asynchronously
        /// </summary>
        /// <param name="task">The task database model with updated values</param>
        Task UpdateTaskAsync(TaskDBModel task);

        /// <summary>
        /// Deletes a task from storage asynchronously
        /// </summary>
        /// <param name="taskId">The unique identifier of the task to delete</param>
        Task DeleteTaskAsync(Guid taskId);
    }
}