using TaskManager.Common.Enums;

namespace TaskManager.DTOModels.ProjectDTO
{
    public class ProjectCreateDTO
    {
        public string Name { get; }
        public string Description { get; }
        public ProjectType ProjectType { get; }

        public ProjectCreateDTO(string name, string description, ProjectType projectType)
        {
            Name = name;
            Description = description;
            ProjectType = projectType;
        }
    }
}