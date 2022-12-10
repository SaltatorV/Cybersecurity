using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cybersecurity.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserTableWithOneTimePassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOneTimePasswordSet",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OneTimePassword",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOneTimePasswordSet",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OneTimePassword",
                table: "Users");
        }
    }
}
