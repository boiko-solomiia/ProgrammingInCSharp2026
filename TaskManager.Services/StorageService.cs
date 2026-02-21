using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TaskManager.DBModels;

namespace TaskManager.Services
{
    /// <summary>
    /// Service class that provides access to storage data.
    /// Loads data from static Storage class on first use.
    /// </summary>
    public class StorageService
    {
        private List<ProjectDBModel> _projects;
        private List<TaskDBModel> _tasks;

        /// <summary>
        /// Loads data from static storage if not already loaded.
        /// Called before any data access operation
        /// </summary>
        private void LoadData()
        {
            if(_projects == null || _tasks == null)
            { 
                _projects = Storage.Projects.ToList();
                _tasks = Storage.Tasks.ToList();
            }
        }

        /// <summary>
        /// Returns all projects from storage.
        /// Loads data first if needed
        /// </summary>
        /// <returns>Collection of all projects</returns>
        public IEnumerable<ProjectDBModel> GetAllProjects()
        {
            LoadData();
            return _projects.ToList();
        }

        /// <summary>
        /// Returns all tasks belonging to a specific project.
        /// Filters tasks by project ID
        /// </summary>
        /// <param name="projectId">ID of the project</param>
        /// <returns>Collection of tasks for the given project</returns>
        public IEnumerable<TaskDBModel> GetTasks(Guid projectId)
        {
            LoadData();
            var resList = new List<TaskDBModel>();
            foreach (var task in _tasks)
            {
                if (task.ProjectId == projectId)
                    resList.Add(task);
            }
            return resList;

        }

    }
}
