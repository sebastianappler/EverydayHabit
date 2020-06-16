# Example usage

## Update database
dotnet ef database update -s ..\EFCoreRunner\EFCoreRunner.csproj

## Migrations
dotnet ef migrations -s ..\EFCoreRunner\EFCoreRunner.csproj add MyMigration

dotnet ef migrations remove -s ..\EFCoreRunner\EFCoreRunner.csproj