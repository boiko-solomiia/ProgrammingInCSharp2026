using TaskManager.Common.Enums;

namespace TaskManager.DTOModels.ProjectDTO
{
    public class ProjectEditDTO
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public ProjectType ProjectType { get; }

        public ProjectEditDTO(Guid id, string name, string description, ProjectType projectType)
        {
            Id = id;
            Name = name;
            Description = description;
            ProjectType = projectType;
        }
    }
}