using TaskManager.DBModels;
using TaskManager.DTOModels.TaskDTO;
using TaskManager.Repositories;

namespace TaskManager.Services
{
    /// <summary>
    /// Service for task-related logic
    /// Retrieves data from the task repository and transforms it into DTOs for the UI layer
    /// Handles computed fields such as overdue status
    /// </summary>
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskService"/> class.
        /// </summary>
        /// <param name="taskRepository">Repository for task data access. Provides raw task entities from the data layer</param>
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TaskListDTO>> GetTasksForProjectAsync(Guid projectId)
        {
            return (await _taskRepository.GetTasksForProjectAsync(projectId)).Select(task => new TaskListDTO(task.Id, task.Name, task.Priority, task.Deadline, task.IsCompleted));
        }

        /// <inheritdoc />
        public async Task<TaskDetailsDTO?> GetTaskAsync(Guid taskId)
        {
            var task = await _taskRepository.GetTaskAsync(taskId);
            if (task == null) return null;
            var isOverdue = !task.IsCompleted && DateTime.UtcNow > task.Deadline.ToUniversalTime();
            return new TaskDetailsDTO(task.Id, task.Name, task.Description, task.Priority, task.Deadline,
                task.IsCompleted, isOverdue);
        }

        /// <inheritdoc />
        public async Task<TaskEditDTO?> GetTaskForEditAsync(Guid taskId)
        {
            var task = await _taskRepository.GetTaskAsync(taskId);
            if (task == null) return null;
            return new TaskEditDTO(task.Id, task.ProjectId, task.Name, task.Description, task.Priority, task.Deadline,
                task.IsCompleted);
        }

        /// <inheritdoc />
        public async Task<Guid> CreateTaskAsync(TaskCreateDTO taskDto)
        {
            ArgumentNullException.ThrowIfNull(taskDto);
            var task = new TaskDBModel(taskDto.ProjectId, taskDto.Name, taskDto.Description, taskDto.Priority,
                taskDto.Deadline, taskDto.IsCompleted);
            await _taskRepository.AddTaskAsync(task);
            return task.Id;
        }

        /// <inheritdoc />
        public async Task UpdateTaskAsync(TaskEditDTO taskDto)
        {
            ArgumentNullException.ThrowIfNull(taskDto);
            var task = await _taskRepository.GetTaskAsync(taskDto.Id);
            if (task == null) return;
            
            if (task.Name == taskDto.Name && 
                task.Description == taskDto.Description && 
                task.Priority == taskDto.Priority &&
                task.Deadline == taskDto.Deadline &&
                task.IsCompleted == taskDto.IsCompleted)
                return;
            
            task.Name = taskDto.Name;
            task.Description = taskDto.Description;
            task.Priority = taskDto.Priority;
            task.Deadline = taskDto.Deadline;
            task.IsCompleted = taskDto.IsCompleted;
            await _taskRepository.UpdateTaskAsync(task);
        }

        /// <inheritdoc />
        public async Task DeleteTaskAsync(Guid taskId)
        {
            var task = await _taskRepository.GetTaskAsync(taskId);

            if (task == null) return;

            await _taskRepository.DeleteTaskAsync(taskId);
        }
    }
}