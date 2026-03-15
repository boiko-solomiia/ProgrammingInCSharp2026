using TaskManager.DTOModels.TaskDTO;

namespace TaskManager.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskListDTO> GetTasksForProject(Guid projectId);
        TaskDetailsDTO GetTask(Guid projectId, Guid taskId);
    }
}