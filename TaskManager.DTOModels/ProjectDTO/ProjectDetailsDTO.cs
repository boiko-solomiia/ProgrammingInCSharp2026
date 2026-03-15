using TaskManager.Common.Enums;

namespace TaskManager.DTOModels.ProjectDTO
{
    public class ProjectDetailsDTO
    {
        public Guid Id { get;}
        public string Name { get;}
        public string Description { get;}
        public ProjectType ProjectType { get;}
        public int Progress { get;}

        public ProjectDetailsDTO(Guid id, string name, string description, ProjectType projectType, int progress)
        {
            Id = id;
            Name = name;
            Description = description;
            ProjectType = projectType;
            Progress = progress;
        }
    }
}