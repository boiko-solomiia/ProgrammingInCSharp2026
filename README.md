# TaskManager

A console application for managing projects and tasks. Developed in C# with .NET 10.0 following layered architecture principles

## Project Structure

```
TaskManager.Common/ # Shared enums (ProjectType, Priority)
TaskManager.DBModels/ # Database entity classes
TaskManager.Services/ # Storage access and fake data
TaskManager.UIModels/ # UI models for create/display/edit operations
TaskManagerConsoleApp/ # Console application
```

## Class Overview

### Database Models (TaskManager.DBModels)

**ProjectDBModel** stores project data: Id, Name, Description, ProjectType
**TaskDBModel** stores task data: Id, ProjectId, Name, Description, Priority, Deadline, IsCompleted

Both classes contain only data fields and constructors. No computed properties or business logic

### UI Models (TaskManager.UIModels)

For each entity, three separate classes handle different responsibilities

**Project Models:**
- `ProjectCreateModel` collects user input for new project and converts to DB model via `ToDbModel()`
- `ProjectDisplayModel` provides read-only view, loads tasks, calculates progress as percentage of completed tasks
- `ProjectEditModel` provides editable view, loads tasks, allows modifications, saves via `SaveChangesToDBModel()`

**Task Models:**
- `TaskCreateModel` collects user input for new task and converts to DB model
- `TaskDisplayModel` provides read-only view and calculates `IsOverdue` based on deadline and completion status
- `TaskEditModel` provides editable view and saves modifications via `SaveChangesToDBModel()`

### Services (TaskManager.Services)

**Storage** is a static class containing fake in-memory data 
**StorageService** is the public API for data access

### Console Application (TaskManagerConsoleApp)

**ConsoleApp** is the main entry point. It displays a menu, handles user input, and coordinates between services and UI models

## Sample Data

The application includes pre-loaded data for testing:
- 6 projects with different types (Educational, Work, Personal, Hobby, Commercial, Team)
- 27 tasks distributed across projects

## Usage Instructions

Run the application and select from the menu:
- `1` - Show all projects
- `2` - Show tasks of a project
- `0` - Exit

When viewing projects, enter the number of a project to see its tasks. Press Enter to return to the main menu after each operation

## Code Documentation

All public classes, methods, and properties include XML comments. Refer to individual files for detailed documentation