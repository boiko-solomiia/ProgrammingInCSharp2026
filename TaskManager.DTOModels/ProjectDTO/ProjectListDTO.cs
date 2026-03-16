using TaskManager.Common.Enums;

namespace TaskManager.DTOModels.ProjectDTO
{
    public class ProjectListDTO
    {
        public Guid Id { get;}
        public string Name { get;}
        public string Description { get; }
        public ProjectType ProjectType { get;}
        public int TaskCount { get;}
        public int Progress { get;}

        public string ProgressDescription => $"Progress: {Progress}%";
        public ProjectListDTO(Guid id, string name, string description, ProjectType projectType, int taskCount, int progress)
        {
            Id = id;
            Name = name;
            Description = description;
            ProjectType = projectType;
            TaskCount = taskCount;
            Progress = progress;
        }
    }
}