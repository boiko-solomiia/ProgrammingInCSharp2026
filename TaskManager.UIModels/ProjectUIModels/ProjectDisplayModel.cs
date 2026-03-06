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
        private readonly IStorageService _storage;
        private readonly ProjectDBModel _dbModel;
        private readonly List<TaskDisplayModel> _tasks;
        private int _progress;
        private bool _tasksLoaded = false;

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
        /// Progress description for UI display.
        /// Shows "Not Loaded" if tasks haven't been loaded yet,
        /// otherwise shows progress percentage
        /// </summary>
        public string ProgressDescription
        {
            get => !_tasksLoaded ? "Not Loaded" : $"{_progress}%";
        }
        
        /// <summary>
        /// Creates display model from database project
        /// </summary>
        /// <param name="dbModel">Project from database</param>
        public ProjectDisplayModel(IStorageService storage, ProjectDBModel dbModel)
        {
            _storage = storage;
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
        public void LoadTasks()
        {
            if (_tasksLoaded) return;
            
            foreach (var task in _storage.GetTasks(Id))
            {
                _tasks.Add(new TaskDisplayModel(task));                
            }
            _tasksLoaded = true;
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