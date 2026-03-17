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
        public IEnumerable<TaskListDTO> GetTasksForProject(Guid projectId)
        {
            foreach (var task in _taskRepository.GetTasksForProject(projectId))
            {
                yield return new TaskListDTO(task.Id, task.Name, task.Priority, task.Deadline, task.IsCompleted);
            }
        }

        /// <inheritdoc />
        public TaskDetailsDTO GetTask(Guid projectId, Guid taskId)
        {
            var task = _taskRepository.GetTask(projectId, taskId);
            if (task == null) return null;
            var isOverdue = !task.IsCompleted && DateTime.UtcNow > task.Deadline.ToUniversalTime();
            return new TaskDetailsDTO(task.Id, task.Name, task.Description, task.Priority, task.Deadline,
                task.IsCompleted, isOverdue);
        }

        /// <inheritdoc />
        public TaskEditDTO GetTaskForEdit(Guid projectId, Guid taskId)
        {
            var task = _taskRepository.GetTask(projectId, taskId);
            if (task == null) return null;
            return new TaskEditDTO(task.Id, task.ProjectId,task.Name, task.Description, task.Priority, task.Deadline, task.IsCompleted);
        }

        /// <inheritdoc />
        public Guid CreateTask(TaskCreateDTO taskDto)
        {
            var task = new TaskDBModel(taskDto.ProjectId, taskDto.Name, taskDto.Description, taskDto.Priority, taskDto.Deadline, taskDto.IsCompleted);
            _taskRepository.AddTask(task);
            return task.Id;
        }

        /// <inheritdoc />
        public void UpdateTask(TaskEditDTO taskDto)
        {
            var task = _taskRepository.GetTask(taskDto.ProjectId, taskDto.Id);
            if (task == null) return;
            task.Name = taskDto.Name;
            task.Description = taskDto.Description;
            task.Priority = taskDto.Priority;
            task.Deadline = taskDto.Deadline;
            task.IsCompleted = taskDto.IsCompleted;
            _taskRepository.UpdateTask(task);
        }
    }
}