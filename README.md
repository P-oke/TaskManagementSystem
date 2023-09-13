
```markdown
# Task Management System API

This is a backend API for a Task Management System that allows users to create, manage, and track tasks and projects, and receive notifications.

## Table of Contents

- [Requirements](#requirements)
- [API Endpoints](#api-endpoints)
- [Clean Architecture](#clean-architecture)
- [Domain-Driven Design (Optional)](#domain-driven-design-optional)
- [SOLID Principles](#solid-principles)
- [Error Handling](#error-handling)
- [Database](#database)
- [.NET Version](#net-version)
- [Background Jobs](#background-jobs)
- [Local Development](#local-development)
- [Swagger](#swagger)
- [Authorization](#authorization)
- [Contributing](#contributing)
- [License](#license)

## Requirements

### Entities & Relationships

- **Task**: Each task has a title, description, due date, priority (low, medium, high), and status (pending, in-progress, completed).
- **Project**: Tasks can be grouped under projects. Each project has a name, description, and a list of associated tasks.
- **User**: Users can create and manage tasks. Each user has basic information such as name, email, and a list of tasks they've created.
- **Notification**: Notifications inform users about upcoming due dates, task status changes, or other relevant events. Each notification has a type (due date reminder, status update), a message, and a timestamp.

### API Endpoints

- CRUD operations for Tasks, Projects, Users, and Notifications.
- Endpoint to fetch tasks based on their status or priority.
- Endpoint to fetch tasks due for the current week.
- Endpoint to assign a task to a project or remove it from a project.
- Endpoint to mark a notification as read or unread.

### Notifications

Users receive notifications when:

- A task's due date is within 48 hours.
- A task they created is marked as completed.
- They are assigned a new task.

### Clean Architecture

The application is structured into relevant layers (e.g., Presentation, Application, Domain, Infrastructure) to ensure separation of concerns.

### SOLID Principles

The application adheres to SOLID principles to showcase object-oriented design understanding.

### Error Handling

Proper error handling is implemented for the API, returning appropriate HTTP status codes and error messages for different types of errors (e.g., validation errors, not found errors).

## Database

The application uses SQL Server as the database backend. To create the database, use Entity Framework Core migrations:

1. Open a terminal/command prompt or nugget package manager console

2. Navigate to the project directory:

   ```bash
   cd taskmanagementsystem
   ```

3. Run the following commands to create and apply migrations:

   ```bash
   dotnet ef migrations add YourMigrationName or Add-Migration YourMigrationName
   dotnet ef database update or Update-Database
   ```

## .NET Version

The project is developed using .NET 6.

## Background Jobs

Hangfire is used for background jobs. Hangfire was used to send notifications for task due dates, task status updates or assignments.

## Local Development

The application is running locally on the following URLs:

- HTTPS: [https://localhost:7152](https://localhost:7152)
- HTTP: [http://localhost:5250](http://localhost:5250)

### Hangfire Dashboard

The Hangfire dashboard is accessible at:

- HTTPS: [https://localhost:7152/job](https://localhost:7152/job)
- HTTP: [http://localhost:5250/job](http://localhost:5250/job)

## Swagger

The application uses Swagger for API documentation. Access the Swagger UI at:

- HTTPS: [https://localhost:7152/swagger/index.html](https://localhost:7152/swagger/index.html)
- HTTP: [http://localhost:5250/swagger/index.html](http://localhost:5250/swagger/index.html)

## Authorization

API endpoints are authorized with JWT tokens. To test endpoints using Swagger, add the JWT token to the "Authorize" section like this: `Bearer your-jwt-token`.

## License

This project is licensed under the [MIT License](LICENSE).

