### Description

This service is responsible for repositories and AppDbContext.

### Migration steps

1. `cd` SystemMonitor\SystemMonitor.DataService
2. Run `dotnet ef migrations add "<Migration description>" --startup-project ..\SystemMonitor.<Startup project>`
3. Run `dotnet ef database update --startup-project ..\SystemMonitor.<Startup project>` 

### Feature improvements

- Fix the problem with two startup projects
- Refactor naming conventions
- Add unit tests
- Create bat or sh script for migration steps
