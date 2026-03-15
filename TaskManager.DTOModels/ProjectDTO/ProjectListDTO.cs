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

        public ProjectListDTO(Guid id, string name, ProjectType projectType, int taskCount, int completedTaskCount)
        {
            Id = id;
            Name = name;
            ProjectType = projectType;
            TaskCount = taskCount;
            if (taskCount == 0) 
            {
                Progress = 0;
            }
            else
            { 
                Progress=  (completedTaskCount * 100) / taskCount;
            }
        }
    }
}