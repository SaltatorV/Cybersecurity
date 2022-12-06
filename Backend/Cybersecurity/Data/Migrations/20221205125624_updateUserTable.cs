using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cybersecurity.Migrations
{
    /// <inheritdoc />
    public partial class updateUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPasswordExpire",
                table: "Users",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordExpire",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPasswordExpire",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordExpire",
                table: "Users");
        }
    }
}
