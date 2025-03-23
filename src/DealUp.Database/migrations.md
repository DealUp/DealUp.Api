# Entity Framework Core migrations guide

## Overview
This guide provides instructions for managing database migrations in the DealUp.Database project using Entity Framework Core tools. The project uses `PostgresqlContext` as the main database context, and all migrations are stored in the Migrations folder at the root project level.

## Prerequisites
- .NET SDK installed
- Entity Framework Core tools installed
- Access to the project's database

## Installing EF Core tools
If you haven't installed the EF Core tools yet, run:
```bash
dotnet tool install --global dotnet-ef
```

## Creating a new migration

1. Navigate to the `DealUp/src/` folder
2. Run the following command:
```bash
dotnet ef migrations add [MigrationName] --context PostgresqlContext --project DealUp.Database --startup-project DealUp.Application.Api --output-dir Migrations
```

Replace `[MigrationName]` with a descriptive name for your migration (use PascalCase), for example:
```bash
dotnet ef migrations add AddUserProfileTable
```

## Applying migrations

To apply all pending migrations to the database:
```bash
dotnet ef database update --context PostgresqlContext --project DealUp.Database --startup-project DealUp.Application.Api
```

To apply migrations up to a specific migration:
```bash
dotnet ef database update [MigrationName] --context PostgresqlContext --project DealUp.Database --startup-project DealUp.Application.Api
```

## Reverting migrations

To undo the last applied migration:
```bash
dotnet ef database update [PreviousMigrationName] --context PostgresqlContext --project DealUp.Database --startup-project DealUp.Application.Api
```

To remove the last migration (if not yet applied to the database):
```bash
dotnet ef migrations remove --context PostgresqlContext --project DealUp.Database --startup-project DealUp.Application.Api
```

## Listing migrations

To see all available migrations and their status:
```bash
dotnet ef migrations list --context PostgresqlContext --project DealUp.Database --startup-project DealUp.Application.Api
```

## Important notes

- All migration files are stored in the `Migrations` folder at the `DealUp.Database` root project level.
- Always commit migration files to source control.
- Consider data preservation when making schema changes.
