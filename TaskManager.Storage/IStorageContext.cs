using TaskManager.DBModels;

namespace TaskManager.Storage
{
    /// <summary>
    /// Provides access to the in-memory data storage
    /// </summary>
    public interface IStorageContext
    {
        /// <summary>
        /// Returns all projects from storage
        /// </summary>
        /// <returns>Collection of all projects</returns>
        IEnumerable<ProjectDBModel> GetAllProjects();

        /// <summary>
        /// Returns all tasks belonging to a specific project
        /// </summary>
        /// <param name="projectId">ID of the project</param>
        /// <returns>Collection of tasks for the given project</returns>
        IEnumerable<TaskDBModel> GetTasksForProject(Guid projectId);

        /// <summary>
        /// Returns a specific project by its ID
        /// </summary>
        /// <param name="projectId">ID of the project</param>
        /// <returns>Project if found, null otherwise</returns>
        ProjectDBModel GetProject(Guid projectId);

        /// <summary>
        /// Returns the total number of tasks for a project
        /// </summary>
        /// <param name="projectId">ID of the project</param>
        /// <returns>Number of tasks</returns>
        int GetTasksCountForProject(Guid projectId);

        /// <summary>
        /// Returns the number of completed tasks for a project
        /// </summary>
        /// <param name="projectId">ID of the project</param>
        /// <returns>Number of completed tasks</returns>
        int GetCompletedTasksCountForProject(Guid projectId);
        void AddProject(ProjectDBModel project);
        void UpdateProject(ProjectDBModel project);
        void AddTask(TaskDBModel task);
        void UpdateTask(TaskDBModel task);
    }
}