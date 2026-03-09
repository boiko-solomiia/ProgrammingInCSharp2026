using TaskManager.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Services
{
    /// <summary>
    /// Provides access to saved projects and tasks
    /// </summary>
    public interface IStorageService
    {
        //// <summary>
        /// Gets all saved projects
        /// </summary>
        /// <returns>A collection of projects</returns>
        public IEnumerable<ProjectDBModel> GetAllProjects();
        /// <summary>
        /// Gets all tasks for the given project
        /// </summary>
        /// <param name="projectId">The ID of the project</param>
        /// <returns>A collection of tasks for this project</returns>
        public IEnumerable<TaskDBModel> GetTasks(Guid projectId);
    }
}
