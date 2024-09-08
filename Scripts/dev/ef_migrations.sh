#To add a new migration:
dotnet ef migrations add <MigrationName>

#To update the database to the latest migration:
dotnet ef database update

#To revert the last applied migration:
dotnet ef migrations remove

#To list all the migrations applied to the database:
dotnet ef migrations list

#To drop the database (note: this will delete all data):
dotnet ef database drop
