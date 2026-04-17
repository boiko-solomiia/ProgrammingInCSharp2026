# TaskManager

A .NET MAUI application for managing projects and tasks. Developed in C# with .NET 10.0 following layered architecture principles, MVVM, asynchronous file storage,DTO models, repositories, services, and dependency injection

## Project Structure

```
TaskManager.Common/ # Shared enums (ProjectType, Priority)
TaskManager.DBModels/ # Database entity classes
TaskManager.DTOModels/ # DTO models for list, details, create, and edit scenarios
TaskManager.Storage/ # In-memory storage context with sample data
TaskManager.Repositories/ # Data access layer working with DB models
TaskManager.Services/ # Business logic and mapping from DB models to DTOs
TaskManager/ # MAUI graphical application (Pages, ViewModels, App, MauiProgram)

```

## Class Overview

### Database Models (TaskManager.DBModels)

**ProjectDBModel** stores project data: Id, Name, Description, ProjectType
**TaskDBModel** stores task data: Id, ProjectId, Name, Description, Priority, Deadline, IsCompleted

Both classes contain only data fields and constructors. No computed properties or business logic

### DTO Models (TaskManager.DTOModels)

DTO models are used to pass data from the service layer to the UI

**Project DTOs:**
- ProjectListDTO contains data needed to display a project in the list
- ProjectDetailsDTO contains full project information for the details page
- ProjectCreateDTO contains data needed to create a new project
- ProjectEditDTO contains data needed to edit an existing project
  
**Task DTOs:**
- TaskListDTO contains data needed to display a task in the list
- TaskDetailsDTO contains full task information for the details page
- TaskCreateDTO contains data needed to create a new task
- TaskEditDTO contains data needed to edit an existing task

### Storage (TaskManager.Storage)

- **IStorageContext**: Interface defining asynchronous operations for projects and tasks
- **InMemoryStorageContext**: In‑memory implementation with sample data (used to seed the file storage)
- **FileStorageContext**: JSON file‑based implementation. Stores each project as a `.json` file and tasks inside project‑specific subdirectories. Automatically initializes with sample data on first run

### Repositories (TaskManager.Repositories)
Repositories work with the storage layer and return DB models

**Project Repository:**
- IProjectRepository - interface for project data access
- ProjectRepository - implementation for working with project data
  
**Task Repository:**
- ITaskRepository - interface for task data access
- TaskRepository - implementation for working with task data

### Services (TaskManager.Services)

**Project Service:**
- IProjectService - interface for project operations
- ProjectService - gets project data, calculates progress, and returns DTO models

**Task Service:**
- ITaskService - interface for task operations
- TaskService - gets task data, calculates overdue status, and returns DTO models. 

### MAUI Application (TaskManager)

**Pages:**
- `ProjectsPage` - displays list of all projects
- `ProjectDetailsPage` - shows project details with tasks
- `CreateProjectPage` / `EditProjectPage` – forms for creating/editing projects
- `TaskDetailsPage` - shows detailed task information
- `CreateTaskPage` / `EditTaskPage` – forms for creating/editing tasks

**ViewModels:**
- ProjectsViewModel - loads projects and handles navigation to project details
- ProjectDetailsViewModel - loads selected project and its tasks
- TaskDetailsViewModel - loads selected task details
- `CreateProjectViewModel`, `EditProjectViewModel`, `CreateTaskViewModel`, `EditTaskViewModel` – manage input forms and validation

All ViewModels inherit from `BaseViewModel`, which provides `IsBusy` and `IsNotBusy` properties for UI state management

## Sample Data

The application includes pre-loaded data for testing:
- 6 projects with different types (Educational, Work, Personal, Hobby, Commercial, Team)
- 27 tasks distributed across projects

## Usage Instructions

### MAUI Application
1. Run the application
2. Browse projects on the main page
3. Tap a project to see its details and tasks
4. Tap a task to see full task information

## Filtering, search and sorting

- **Projects Page**:
  - Search by project name
  - Filter by project type
  - Sort by name, progress, or task count
- **Project Details Page**:
  - Search tasks by name
  - Filter by priority
  - Toggle to show only completed tasks
  - Sort by name, priority, or deadline
- Each page includes **Apply** and **Reset** buttons for easy filter management

## Code Documentation

All public classes, methods, and properties include XML comments. Refer to individual files for detailed documentation
