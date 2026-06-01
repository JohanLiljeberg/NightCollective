# NightCollective

NightCollective is an ASP.NET Core MVC website starter for a collective focused on celebrating game creation and games as art.

## Project structure

- `Controllers/` keeps HTTP actions thin and delegates page composition to services.
- `Models/` contains domain models such as projects, events, and collective members.
- `ViewModels/` contains page-specific data contracts for Razor views.
- `Services/` contains business and presentation orchestration logic.
- `Repositories/` contains data access abstractions and implementations. The starter currently uses an in-memory repository so the site can run before a database is selected.
- `Extensions/` contains dependency injection registration helpers.
- `Views/` contains Razor MVC views.
- `wwwroot/` contains static assets such as CSS and JavaScript.

## Current MVC flow

1. `HomeController` receives the request.
2. `ICollectiveService` builds a page view model.
3. `ICollectiveRepository` supplies featured projects, events, and member profile data.
4. `Views/Home/Index.cshtml` renders the collective homepage.

## Next recommended steps

- Replace `InMemoryCollectiveRepository` with a database-backed implementation when content needs persistence.
- Add admin or CMS workflows for projects, events, and member profiles.
- Add automated tests for services and repository implementations.
- Expand the design system with reusable partials for cards, event listings, and calls to action.
