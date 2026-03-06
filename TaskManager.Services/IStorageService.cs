using TaskManager.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Services
{
    public interface IStorageService
    {
        public IEnumerable<ProjectDBModel> GetAllProjects();
        public IEnumerable<TaskDBModel> GetTasks(Guid projectId);
    }
}
