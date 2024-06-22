using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstagramCloneAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert test data into Users table
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Username", "Email", "PasswordHash" },
                values: new object[,]
                {
                    { "1", "user1", "user1@example.com", "passwordHash1" },
                    { "2", "user2", "user2@example.com", "passwordHash2" },
                    { "3", "user3", "user3@example.com", "passwordHash3" }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove test data from Users table
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValues: new object[] { "1", "2", "3" }
            );
        }
    }
}
