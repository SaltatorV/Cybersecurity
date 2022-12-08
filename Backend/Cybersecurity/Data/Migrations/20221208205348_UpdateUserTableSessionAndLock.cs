using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cybersecurity.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserTableSessionAndLock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FailLoginCounter",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLoginLockOn",
                table: "Users",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LoginLockOnTime",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxFailLogin",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SessionTime",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailLoginCounter",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsLoginLockOn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LoginLockOnTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MaxFailLogin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SessionTime",
                table: "Users");
        }
    }
}
