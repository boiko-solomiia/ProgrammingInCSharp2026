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
        /// Retrieves all tasks belonging to a specific project
        /// </summary>
        /// <param name="projectId">The unique identifier of the project</param>
        /// <returns>A collection of task database models for the specified project</returns>
        IEnumerable<TaskDBModel> GetTasksForProject(Guid projectId);

        /// <summary>
        /// Retrieves a specific task within a project by its unique identifier
        /// </summary>
        /// <param name="projectId">The unique identifier of the project containing the task</param>
        /// <param name="taskId">The unique identifier of the task</param>
        /// <returns>The task database model if found; otherwise, null</returns>
        TaskDBModel GetTask(Guid projectId, Guid taskId);

        /// <summary>
        /// Gets the total number of tasks for a specific project
        /// </summary>
        /// <param name="projectId">The unique identifier of the project</param>
        /// <returns>The count of all tasks in the project</returns>
        int GetTasksCountForProject(Guid projectId);

        /// <summary>
        /// Gets the number of completed tasks for a specific project
        /// </summary>
        /// <param name="projectId">The unique identifier of the project</param>
        /// <returns>The count of completed tasks in the project</returns>
        int GetCompletedTasksCountForProject(Guid projectId);

        void AddTask(TaskDBModel task);
        void UpdateTask(TaskDBModel task);
    }
}