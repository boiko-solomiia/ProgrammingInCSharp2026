using TaskManager.Common.Enums;
using TaskManager.DBModels;
using TaskManager.Services;
using TaskManager.UIModels.TaskUIModels;

namespace TaskManager.UIModels.ProjectUIModels
{
    /// <summary>
    /// UI model for editing existing projects.
    /// Stores local copy of project data and allows loading associated tasks.
    /// All changes can be saved back to the database via SaveChangesToDBModel()
    /// </summary>
    public class ProjectEditModel
    {
        private readonly ProjectDBModel _dbModel;
        private string _name;
        private string _description;
        private ProjectType _projectType;
        private readonly List<TaskEditModel> _tasks;

        /// <summary>
        /// Unique project identifier (from database model).
        /// Cannot be edited
        /// </summary>
        public Guid Id
        {
            get => _dbModel.Id;
        }

        /// <summary>
        /// Project name (editable)
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        /// <summary>
        /// Project description (editable)
        /// </summary>
        public string Description
        {
            get => _description;
            set => _description = value;
        }

        /// <summary>
        /// Project type (editable)
        /// </summary>
        public ProjectType ProjectType
        {
            get => _projectType;
            set => _projectType = value;
        }

        /// <summary>
        /// Read-only collection of project tasks available for editing
        /// </summary>
        public IReadOnlyList<TaskEditModel> Tasks
        {
            get => _tasks;
        }

        /// <summary>
        /// Creates edit model from existing project
        /// </summary>
        /// <param name="dbModel">Existing project from database</param>
        public ProjectEditModel(ProjectDBModel dbModel)
        {
            _dbModel = dbModel;
            _name = dbModel.Name;
            _description = dbModel.Description;
            _projectType = dbModel.ProjectType;
            _tasks = new List<TaskEditModel>();
        }

        /// <summary>
        /// Saves all changes to the underlying database models.
        /// Updates project properties and all modified tasks
        /// </summary>
        public void SaveChangesToDBModel()
        {

            _dbModel.Name = _name;
            _dbModel.Description = _description;
            _dbModel.ProjectType = _projectType;
            foreach (var task in _tasks)
            {
                task.SaveChangesToDBModel(); 
            }
        }

        /// <summary>
        /// Loads tasks for this project from storage.
        /// Only loads if tasks haven't been loaded yet
        /// </summary>
        /// <param name="storage">Storage service to load tasks from</param>
        public void LoadTasks(StorageService storage)
        {
            if (_tasks.Count > 0)
            {
                return;
            }
            var taskDbModels = storage.GetTasks(Id);
            foreach (var taskDb in taskDbModels)
            {
                _tasks.Add(new TaskEditModel(taskDb)); 
            }
        }
        
        /// <summary>
        /// Reloads tasks from storage, discarding any local changes
        /// </summary>
        public void RefreshTasks(StorageService storage)
        {
            _tasks.Clear();  
            var taskDbModels = storage.GetTasks(Id);
            foreach (var taskDb in taskDbModels)
            {
                _tasks.Add(new TaskEditModel(taskDb)); 
            }
        }
    }
}