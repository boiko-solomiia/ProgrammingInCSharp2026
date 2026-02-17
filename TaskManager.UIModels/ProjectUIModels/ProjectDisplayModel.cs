using TaskManager.Common.Enums;
using TaskManager.DBModels;
using TaskManager.UIModels.TaskUIModels;

namespace TaskManager.UIModels.ProjectUIModels
{
    public class ProjectDisplayModel
    {
        private readonly ProjectDBModel _dbModel;
        private readonly List<TaskDisplayModel> _tasks;
        private int _progress;

        public Guid Id
        {
            get => _dbModel.Id;
        }

        public string Name
        {
            get  => _dbModel.Name;
        }

        public string Description
        {
            get  => _dbModel.Description;
        }

        public ProjectType ProjectType
        {
            get  => _dbModel.ProjectType;
        }

        public IReadOnlyList<TaskDisplayModel> Tasks
        {
            get  => _tasks;
        }

        public int Progress
        {
            get  => _progress;
        }
        
        public ProjectDisplayModel(ProjectDBModel dbModel)
        {
            _dbModel = dbModel;
            _tasks = new List<TaskDisplayModel>();
            CalculateProgress();
        }

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
    }
}