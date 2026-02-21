using TaskManager.Common.Enums;
using TaskManager.DBModels;

namespace TaskManager.Services
{
    internal static class Storage
    {
        private static readonly List<ProjectDBModel> _projects;
        private static readonly List<TaskDBModel> _tasks;

        internal static IEnumerable<ProjectDBModel> Projects
        {
            get => _projects.ToList();
        }

        internal static IEnumerable<TaskDBModel> Tasks
        {
            get => _tasks.ToList();
        }

        static Storage()
        {
            _projects = new List<ProjectDBModel>();
            _tasks = new List<TaskDBModel>();
            LoadInitialData();
        }
        
        private static void LoadInitialData()
        {
            var airclaySculpting = new ProjectDBModel(
                "Airclay sculpting",
                "First attempt to make sculpture from air-dry clay",
                ProjectType.Hobby);
            _projects.Add(airclaySculpting);

            var surpriseParty = new ProjectDBModel(
                "Oksana's surprise party",
                "Organizing a surprise party for Oksana on March 8",
                ProjectType.Team);
            _projects.Add(surpriseParty);

            var summerTrip = new ProjectDBModel(
                "Summer trip to Carpathians", 
                "Plan summer vacation in the Carpathian mountains",
                ProjectType.Personal);
            _projects.Add(summerTrip);
            
            var alumniBanner = new ProjectDBModel("KMA Alumni Banner",
                "Design banner for KMA alumni and prepare files for printing",
                ProjectType.Work);
            _projects.Add(alumniBanner);
            
            var laboratory = new ProjectDBModel(
                "C# laboratory 1",
                "First laboratory work in C# in team" ,
                ProjectType.Educational);
            _projects.Add(laboratory);
            
            var traditionalBeadwork = new ProjectDBModel(
                "Ukrainian gerdan",
                "Creating authentic Ukrainian traditional beaded jewelry - a gerdan (for a sale)",
                ProjectType.Commercial);
            _projects.Add(traditionalBeadwork);
            
            _tasks.Add(new TaskDBModel(airclaySculpting.Id, "Watch tutorials", "Find and watch YouTube tutorials on air-dry clay basics", Priority.Low, new DateTime(2026, 3, 14), true));
            _tasks.Add(new TaskDBModel(airclaySculpting.Id, "Buy materials","Purchase air-dry clay and basic tools", Priority.Low, new DateTime(2026, 3, 15), false));
            _tasks.Add(new TaskDBModel(airclaySculpting.Id, "Make first sculpture", "Try making a simple sculpture of swan", Priority.Low, new DateTime(2026, 3, 21), false));
            
            _tasks.Add(new TaskDBModel(surpriseParty.Id, "Choose venue", "Find and book a place for March 8", Priority.High, new DateTime(2026, 2, 25), true));
            _tasks.Add(new TaskDBModel(surpriseParty.Id, "Invite guests", "Contact all friends without spoiling the surprise", Priority.High, new DateTime(2026, 3, 1), false));
            _tasks.Add(new TaskDBModel(surpriseParty.Id, "Buy decorations", "Get balloons and birthday decorations", Priority.Medium, new DateTime(2026, 3, 5), false));
            _tasks.Add(new TaskDBModel(surpriseParty.Id, "Order cake", "Order birthday cake with her name (ideally with a chocolate-strawberry filling)", Priority.High, new DateTime(2026, 3, 3), false));
            
            _tasks.Add(new TaskDBModel(summerTrip.Id, "Research locations", "Look for interesting places in Carpathians", Priority.Medium, new DateTime(2026, 6, 1), false));
            _tasks.Add(new TaskDBModel(summerTrip.Id, "Book accommodation", "Find and book places to stay", Priority.High, new DateTime(2026, 6, 12), false));
            _tasks.Add(new TaskDBModel(summerTrip.Id, "Plan hiking routes", "Choose hiking trails suitable for beginners", Priority.Low, new DateTime(2026, 6, 18), false));
            
            _tasks.Add(new TaskDBModel(alumniBanner.Id, "Create draft designs", "Prepare 2-3 draft concepts for feedback", Priority.High, new DateTime(2026, 2, 14), true));
            _tasks.Add(new TaskDBModel(alumniBanner.Id, "Prepare print files", "Export final design with correct dimensions", Priority.High, new DateTime(2026, 3, 22), false));
            
            _tasks.Add(new TaskDBModel(laboratory.Id, "Online meeting", "Schedule meeting to discuss tasks distribution", Priority.High, new DateTime(2026, 2, 10), true));
            _tasks.Add(new TaskDBModel(laboratory.Id, "Split tasks", "Divide tasks between members", Priority.High, new DateTime(2026, 2, 12), true));
            _tasks.Add(new TaskDBModel(laboratory.Id, "Setup repository", "Create and configure git hub repository with proper gitignore", Priority.High, new DateTime(2026, 2, 15), true));
            _tasks.Add(new TaskDBModel(laboratory.Id, "Create DBModels", "Do database model classes for Project and Task", Priority.High, new DateTime(2026, 2, 12), true));
            _tasks.Add(new TaskDBModel(laboratory.Id, "Create UIModels", "Do display, create and edit models for both entities", Priority.High, new DateTime(2026, 3, 15), true));
            _tasks.Add(new TaskDBModel(laboratory.Id, "Create StorageService", "Implement fake storage and service for working with storage", Priority.Critical, new DateTime(2026, 3, 18), false));
            _tasks.Add(new TaskDBModel(laboratory.Id, "Create console app", "Implement console application", Priority.Critical, new DateTime(2026, 2, 19), false));
            _tasks.Add(new TaskDBModel(laboratory.Id, "Add comments", "Write comments for important code parts", Priority.Critical, new DateTime(2026, 2, 19), false));
            _tasks.Add(new TaskDBModel(laboratory.Id, "Write README", "Create README file with project description", Priority.Critical, new DateTime(2026, 2, 19), false));
            _tasks.Add(new TaskDBModel(laboratory.Id, "Final review", "Check all requirements and prepare for submission", Priority.Critical, new DateTime(2026, 2, 19), false));
            
            _tasks.Add(new TaskDBModel(traditionalBeadwork.Id, "Research traditional patterns", "Find information about authentic Ukrainian gerdan patterns and their symbolism", Priority.Backlog, new DateTime(2026, 3, 10), false));
            _tasks.Add(new TaskDBModel(traditionalBeadwork.Id, "Create gerdan pattern scheme", "Make a detailed scheme with traditional Ukrainian ornament", Priority.Backlog, new DateTime(2026, 3, 15), false));
            _tasks.Add(new TaskDBModel(traditionalBeadwork.Id, "Purchase beads", "Buy beads in chosen colors", Priority.Backlog, new DateTime(2026, 3, 20), false));
            _tasks.Add(new TaskDBModel(traditionalBeadwork.Id, "Get needle and thread", "Special beading needle and strong thread (polyester or nylon)", Priority.Backlog, new DateTime(2026, 3, 22), false));
            _tasks.Add(new TaskDBModel(traditionalBeadwork.Id, "Create gerdan", "Weave the gerdan according to the prepared pattern", Priority.Backlog, new DateTime(2026, 4, 10), false));
        }
    }
}