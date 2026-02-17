using TaskManager.Common.Enums;

namespace TaskManager.DBModels
{
    public class ProjectDBModel
    {
        public Guid Id { get;}
        public string Name { get; set; }
        public string Description { get; set; }
        public ProjectType ProjectType { get; set; }

        private ProjectDBModel() { }

        public ProjectDBModel(string name, string description, ProjectType projectType)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            ProjectType = projectType;
        }
        
        public ProjectDBModel(string name, ProjectType projectType) 
            : this(name, string.Empty, projectType)
        {
        }
    }
}