# Example usage

## Migration projects
You have to run ef core commands from the mgiration project depending on the database you wish to use.

### SQL
cd EverydayHabit\Src\Infrastructure\Persistence.SQL

### SQLite
cd EverydayHabit\Src\Infrastructure\Persistence.SQLite

## Update database
dotnet ef database update -s ..\EFCoreRunner\EFCoreRunner.csproj

## Migrations
cd \EverydayHabit\Src\Infrastructure\Persistence.SQL
dotnet ef migrations -s ..\EFCoreRunner\EFCoreRunner.csproj add MyMigration

dotnet ef migrations remove -s ..\EFCoreRunner\EFCoreRunner.csproj