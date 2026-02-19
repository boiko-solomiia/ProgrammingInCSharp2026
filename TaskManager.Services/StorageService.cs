using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TaskManager.DBModels;

namespace TaskManager.Services
{
    public class StorageService
    {
        private List<ProjectDBModel> _projects;
        private List<TaskDBModel> _tasks;

        private void LoadData()
        {
            if (_projects == null || _tasks == null)
            { 
            _projects = Storage.Projects.ToList();
            _tasks = Storage.Tasks.ToList();
            }
        }

        public IEnumerable<ProjectDBModel> GetAllProjects()
        {
            LoadData();
            return _projects.ToList();
        }

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
