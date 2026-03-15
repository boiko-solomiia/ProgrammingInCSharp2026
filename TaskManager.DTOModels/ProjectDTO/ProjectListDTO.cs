using TaskManager.Common.Enums;

namespace TaskManager.DTOModels.ProjectDTO
{
    public class ProjectListDTO
    {
        public Guid Id { get;}
        public string Name { get;}
        public ProjectType ProjectType { get;}
        public int TaskCount { get;}
        public int Progress { get;}

        public ProjectListDTO(Guid id, string name, ProjectType projectType, int taskCount, int progress)
        {
            Id = id;
            Name = name;
            ProjectType = projectType;
            TaskCount = taskCount;
            Progress = progress;
        }
    }
}