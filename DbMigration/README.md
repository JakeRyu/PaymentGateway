# Database Migration

Fluent Migrator is a migration framework for .NET much like Ruby on Rails Migrations. Migrations are a structured way to alter your database schema and are an alternative to creating lots of sql scripts that have to be run manually by every developer involved. Migrations solve the problem of evolving a database schema for multiple databases (for example, the developer's local database, the test database and the production database). Database schema changes are described in classes written in C# that can be checked into a version control system.

To run migrations out of process, dotnet-fm tool is required. To install, execute following command.

````
>  dotnet tool install -g FluentMigrator.DotNet.Cli
````

The database migration must be performed before running API. In order to do so, use a build script.

1. Open a PowerShell console and go to the solution root folder.
2. Run the build script
````
>  ./build.ps1
```` 