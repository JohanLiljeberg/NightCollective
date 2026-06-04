# NightCollective

NightCollective is an ASP.NET Core MVC website starter for a collective focused on celebrating game creation and games as art.

## Project structure

- `Controllers/` keeps HTTP actions thin and delegates page composition to services.
- `Models/` contains domain models such as projects, events, and collective members.
- `ViewModels/` contains page-specific data contracts for Razor views.
- `Services/` contains business and presentation orchestration logic.
- `Repositories/` contains data access abstractions and implementations. The site now uses Entity Framework Core with a SQL Server-backed repository for persistent collective content.
- `Extensions/` contains dependency injection registration helpers.
- `Views/` contains Razor MVC views.
- `wwwroot/` contains static assets such as CSS and JavaScript.

## Current MVC flow

1. `HomeController` receives the request.
2. `ICollectiveService` builds a page view model.
3. `SqlCollectiveRepository` reads featured projects, events, and member profile data from SQL Server through `AppDbContext`.
4. `Views/Home/Index.cshtml` renders the collective homepage.

## SQL Server content database

The app expects a `NightCollectiveDatabase` connection string. By default, `appsettings.json` points to a local SQL Server database named `NightCollective`, and `Database:EnsureCreatedOnStartup` creates the schema with seeded projects, events, and developer profile data when the app starts. Override the connection string on your server with configuration or an environment variable such as `ConnectionStrings__NightCollectiveDatabase`.

## Next recommended steps

- Add a deployment-specific SQL Server connection string before publishing to your local server.
- Add admin or CMS workflows for projects, events, and member profiles.
- Add automated tests for services and repository implementations.
- Expand the design system with reusable partials for cards, event listings, and calls to action.
