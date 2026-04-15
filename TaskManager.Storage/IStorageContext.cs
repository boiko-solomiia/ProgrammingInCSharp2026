using TaskManager.DBModels;

namespace TaskManager.Storage
{
    /// <summary>
    /// Provides access to the in-memory data storage
    /// </summary>
    public interface IStorageContext
    {
        /// <summary>
        /// Returns all projects from storage asynchronously
        /// </summary>
        /// <returns>Collection of all projects</returns>
        IAsyncEnumerable<ProjectDBModel> GetAllProjectsAsync();

        /// <summary>
        /// Returns all tasks belonging to a specific project asynchronously
        /// </summary>
        /// <param name="projectId">ID of the project</param>
        /// <returns>Collection of tasks for the given project</returns>
        Task<IEnumerable<TaskDBModel>> GetTasksForProjectAsync(Guid projectId);

        /// <summary>
        /// Returns a specific project by its ID asynchronously
        /// </summary>
        /// <param name="projectId">ID of the project</param>
        /// <returns>Project if found, null otherwise</returns>
        Task<ProjectDBModel?> GetProjectAsync(Guid projectId);
        
        /// <summary>
        /// Returns a specific task by its ID asynchronously
        /// </summary>
        /// <param name="taskId">ID of the task</param>
        /// <returns>Task if found, null otherwise</returns>
        Task<TaskDBModel?> GetTaskAsync(Guid taskId);

        /// <summary>
        /// Returns the total number of tasks for a project asynchronously
        /// </summary>
        /// <param name="projectId">ID of the project</param>
        /// <returns>Number of tasks</returns>
        Task<int> GetTasksCountForProjectAsync(Guid projectId);

        /// <summary>
        /// Returns the number of completed tasks for a project asynchronously
        /// </summary>
        /// <param name="projectId">ID of the project</param>
        /// <returns>Number of completed tasks</returns>
        Task<int> GetCompletedTasksCountForProjectAsync(Guid projectId);

        /// <summary>
        /// Adds a new project to the storage asynchronously
        /// </summary>
        /// <param name="project">The project database model to add</param>
        Task AddProjectAsync(ProjectDBModel project);

        /// <summary>
        /// Updates an existing project in the storage asynchronously
        /// </summary>
        /// <param name="project">The project database model with updated values</param>
        Task UpdateProjectAsync(ProjectDBModel project);

        /// <summary>
        /// Adds a new task to the storage asynchronously
        /// </summary>
        /// <param name="task">The task database model to add</param>
        Task AddTaskAsync(TaskDBModel task);

        /// <summary>
        /// Updates an existing task in the storage asynchronously
        /// </summary>
        /// <param name="task">The task database model with updated values</param>
        Task UpdateTaskAsync(TaskDBModel task);

        /// <summary>
        /// Deletes a task from storage asynchronously
        /// </summary>
        /// <param name="taskId">ID of the task to delete</param>
        Task DeleteTaskAsync(Guid taskId);

        /// <summary>
        /// Deletes a project and all its tasks from storage asynchronously
        /// </summary>
        /// <param name="projectId">ID of the project to delete</param>
        Task DeleteProjectAsync(Guid projectId);
    }
}