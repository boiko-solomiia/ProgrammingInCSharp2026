using System.Text.Json.Serialization;
using TaskManager.Common.Enums;

namespace TaskManager.DBModels
{
    /// <summary>
    /// Database model representing a project entity.
    /// Stores core project data without business logic or computed fields
    /// </summary>
    public class ProjectDBModel
    {
        /// <summary>
        /// Unique project identifier. Generated once and cannot be changed.
        /// </summary>
        public Guid Id { get;}
        
        /// <summary>
        /// Project name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Detailed project description
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Project type/category (Educational, Work, Personal, etc.)
        /// </summary>
        public ProjectType ProjectType { get; set; }
        
        private ProjectDBModel() { }
        
        /// <summary>
        /// Creates a project with specified identifier.
        /// Used when reconstructing projects from storage with existing IDs.
        /// </summary>
        /// <param name="id">Project identifier</param>
        /// <param name="name">Project name</param>
        /// <param name="description">Project description</param>
        /// <param name="projectType">Project type</param>
        [JsonConstructor]
        public ProjectDBModel(Guid id, string name, string description, ProjectType projectType)
        {
            Id = id;
            Name = name;
            Description = description;
            ProjectType = projectType;
        }
        
        /// <summary>
        /// Creates a new project with all fields.
        /// Generates a new unique identifier automatically.
        /// </summary>
        /// <param name="name">Project name</param>
        /// <param name="description">Project description</param>
        /// <param name="projectType">Project type</param>
        public ProjectDBModel(string name, string description, ProjectType projectType) 
            : this(Guid.NewGuid(), name, description, projectType)
        {
        }
        
        /// <summary>
        /// Creates a new project with empty description.
        /// Generates a new unique identifier automatically.
        /// </summary>
        /// <param name="name">Project name</param>
        /// <param name="projectType">Project type</param>
        public ProjectDBModel(string name, ProjectType projectType) 
            : this(name, string.Empty, projectType)
        {
        }
    }
}