using TaskManager.Common.Enums;
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
        /// Retrieves all tasks for a specific project, formatted for list views asynchronously
        /// </summary>
        /// <param name="projectId">The unique identifier of the project.</param>
        /// <returns>A collection of tasks with fields optimized for listing (e.g., name, priority, deadline).</returns>
        Task<IEnumerable<TaskListDTO>> GetTasksForProjectAsync(Guid projectId);

        /// <summary>
        /// Retrieves detailed information about a specific task within a project asynchronously
        /// Includes computed fields like overdue status.
        /// </summary>
        /// <param name="taskId">The unique identifier of the task.</param>
        /// <returns>Detailed task information including description and overdue status, or null if not found.</returns>
        Task<TaskDetailsDTO?> GetTaskAsync(Guid taskId);

        /// <summary>
        /// Gets editable data for a specific task within a project asynchronously
        /// </summary>
        /// <param name="taskId">The unique identifier of the task to edit</param>
        /// <returns>Task data formatted for editing, or null if task not found</returns>
        Task<TaskEditDTO?> GetTaskForEditAsync(Guid taskId);

        /// <summary>
        /// Creates a new task from the provided DTO asynchronously
        /// </summary>
        /// <param name="taskDto">The task data transfer object containing creation information</param>
        /// <returns>The unique identifier of the newly created task</returns>
        Task<Guid> CreateTaskAsync(TaskCreateDTO taskDto);

        /// <summary>
        /// Updates an existing task using the provided DTO asynchronously
        /// </summary>
        /// <param name="taskDto">The task data transfer object containing updated values</param>
        Task UpdateTaskAsync(TaskEditDTO taskDto);

        /// <summary>
        /// Deletes a task asynchronously
        /// </summary>
        /// <param name="taskId">The unique identifier of the task to delete</param>
        Task DeleteTaskAsync(Guid taskId);
        
        /// <summary>
        /// Retrieves tasks for a specific project, filtered by various criteria and sorted by the specified option
        /// </summary>
        /// <param name="projectId">The unique identifier of the parent project</param>
        /// <param name="searchName">Text to search in task name. If null or empty, no search filter is applied</param>
        /// <param name="priority">Specific priority to filter by. If null, all priorities are included</param>
        /// <param name="isCompleted">Completion status filter. If null, both completed and uncompleted tasks are included</param>
        /// <param name="sortOption">Sorting option for the result list. Defaults to <see cref="TaskSortOption.PriorityDesc"/></param>
        /// <returns>A collection of <see cref="TaskListDTO"/> matching the criteria</returns>
        Task<IEnumerable<TaskListDTO>> GetTasksFilteredAsync(Guid projectId, string? searchName, Priority? priority, bool? isCompleted, TaskSortOption sortOption = TaskSortOption.PriorityDesc);
    }
}