using TaskManager.DTOModels.TaskDTO;

namespace TaskManager.Services
{
    /// <summary>
    /// Provides operations for retrieving task data prepared for the UI layer
    /// Defines the contract for task-related logic and data transformation
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        /// Retrieves all tasks for a specific project, formatted for list views.
        /// </summary>
        /// <param name="projectId">The unique identifier of the project.</param>
        /// <returns>A collection of tasks with fields optimized for listing (e.g., name, priority, deadline).</returns>
        IEnumerable<TaskListDTO> GetTasksForProject(Guid projectId);

        /// <summary>
        /// Retrieves detailed information about a specific task within a project.
        /// Includes computed fields like overdue status.
        /// </summary>
        /// <param name="projectId">The unique identifier of the project containing the task.</param>
        /// <param name="taskId">The unique identifier of the task.</param>
        /// <returns>Detailed task information including description and overdue status, or null if not found.</returns>
        TaskDetailsDTO GetTask(Guid projectId, Guid taskId);

        /// <summary>
        /// Gets editable data for a specific task within a project
        /// </summary>
        /// <param name="projectId">The unique identifier of the project containing the task</param>
        /// <param name="taskId">The unique identifier of the task to edit</param>
        /// <returns>Task data formatted for editing, or null if task not found</returns>
        TaskEditDTO GetTaskForEdit(Guid projectId, Guid taskId);

        /// <summary>
        /// Creates a new task from the provided DTO
        /// </summary>
        /// <param name="taskDto">The task data transfer object containing creation information</param>
        /// <returns>The unique identifier of the newly created task</returns>
        Guid CreateTask(TaskCreateDTO taskDto);

        /// <summary>
        /// Updates an existing task using the provided DTO
        /// </summary>
        /// <param name="taskDto">The task data transfer object containing updated values</param>
        void UpdateTask(TaskEditDTO taskDto);
    }
}