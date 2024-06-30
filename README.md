# InstagramCloneAPI

## Database Migration

In .NET, the Entity Framework Core (EF Core) provides a set of commands for managing database migrations. Here are some common `dotnet ef` commands you might use for managing migrations:

1. **Install the Tools:**
   Before running migration commands, ensure that you have the EF Core tools installed. You can install them using the following command:
   ```bash
   dotnet tool install --global dotnet-ef
   ```

2. **Add a Migration:**
   To create a new migration, use the `add` command followed by the name of the migration:
   ```bash
   dotnet ef migrations add InitialCreate
   ```
   Replace `InitialCreate` with a descriptive name for your migration.

3. **Remove a Migration:**
   If you need to remove the last migration, use the `remove` command:
   ```bash
   dotnet ef migrations remove
   ```

4. **Update the Database:**
   To apply the pending migrations to the database, use the `update` command:
   ```bash
   dotnet ef database update
   ```
   You can also specify a particular migration to update to:
   ```bash
   dotnet ef database update InitialCreate
   ```

5. **Generate a SQL Script:**
   To generate a SQL script for the migrations, use the `script` command:
   ```bash
   dotnet ef migrations script
   ```
   You can specify a range of migrations to include in the script:
   ```bash
   dotnet ef migrations script InitialCreate MyMigration
   ```

6. **List Migrations:**
   To list all the migrations that have been applied to the database, use the `list` command:
   ```bash
   dotnet ef migrations list
   ```

7. **Create a Database:**
   To create the database using the current model, use the `create` command:
   ```bash
   dotnet ef database update
   ```

8. **Drop a Database:**
   To drop the database, use the `drop` command:
   ```bash
   dotnet ef database drop
   ```

### Example Workflow

1. **Create a New Migration:**
   ```bash
   dotnet ef migrations add AddNewTable
   ```

2. **Apply the Migration:**
   ```bash
   dotnet ef database update
   ```

3. **Generate SQL Script for Migration:**
   ```bash
   dotnet ef migrations script
   ```

4. **List Applied Migrations:**
   ```bash
   dotnet ef migrations list
   ```

5. **Remove the Last Migration (if needed):**
   ```bash
   dotnet ef migrations remove
   ```

## OIDC Auth Code Flow