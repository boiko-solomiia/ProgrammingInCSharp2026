using TaskManager.Common.Enums;
using TaskManager.DBModels;
using TaskManager.Services;
using TaskManager.UIModels.TaskUIModels;

namespace TaskManager.UIModels.ProjectUIModels
{
    /// <summary>
    /// UI model for displaying project information with its tasks and progress.
    /// Created from ProjectDBModel and used for read-only views
    /// </summary>
    public class ProjectDisplayModel
    {
        private readonly ProjectDBModel _dbModel;
        private readonly List<TaskDisplayModel> _tasks;
        private int _progress;

        /// <summary>
        /// Unique project identifier
        /// </summary>
        public Guid Id
        {
            get => _dbModel.Id;
        }

        /// <summary>
        /// Project name
        /// </summary>
        public string Name
        {
            get  => _dbModel.Name;
        }

        /// <summary>
        /// Project description
        /// </summary>
        public string Description
        {
            get  => _dbModel.Description;
        }

        /// <summary>
        /// Project type/category
        /// </summary>
        public ProjectType ProjectType
        {
            get  => _dbModel.ProjectType;
        }

        /// <summary>
        /// Read-only collection of tasks belonging to this project
        /// </summary>
        public IReadOnlyList<TaskDisplayModel> Tasks
        {
            get  => _tasks;
        }

        /// <summary>
        /// Project completion percentage (calculated from completed tasks)
        /// </summary>
        public int Progress
        {
            get  => _progress;
        }
        
        /// <summary>
        /// Creates display model from database project
        /// </summary>
        /// <param name="dbModel">Project from database</param>
        public ProjectDisplayModel(ProjectDBModel dbModel)
        {
            _dbModel = dbModel;
            _tasks = new List<TaskDisplayModel>();
            CalculateProgress();
        }

        /// <summary>
        /// Calculates project progress based on completed tasks
        /// </summary>
        private void CalculateProgress()
        {
            if (_tasks.Count == 0)
            {
                _progress = 0;
                return;
            }

            int tasksCompleted = 0;
            foreach (TaskDisplayModel task in _tasks)
            {
                if (task.IsCompleted) tasksCompleted++;
            }
            _progress = (tasksCompleted * 100) / _tasks.Count;
        }

        /// <summary>
        /// Loads tasks for this project from storage
        /// </summary>
        /// <param name="storage">Storage service</param>
        /// <param name="forceReload">If true, reloads tasks even if already loaded</param>
        public void LoadTasks(StorageService storage, bool forceReload = false)
        {
            if (forceReload)
                _tasks.Clear();
            else if (_tasks.Count > 0)
                return;

            foreach (var task in storage.GetTasks(Id))
            {
                _tasks.Add(new TaskDisplayModel(task));                
            }
    
            CalculateProgress();
        }

        /// <summary>
        /// Returns string representation of project with progress info
        /// </summary>
        public override string ToString()
        {
            return $"Project: {Name} ({_tasks.Count} tasks, {Progress}% complete)";
        }
    }
}