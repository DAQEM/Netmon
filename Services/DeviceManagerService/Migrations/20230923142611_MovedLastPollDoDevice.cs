using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeviceManagerService.Migrations
{
    /// <inheritdoc />
    public partial class MovedLastPollDoDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPolling",
                table: "Disks");

            migrationBuilder.DropColumn(
                name: "PollingInterval",
                table: "Disks");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPoll",
                table: "Devices",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPoll",
                table: "Devices");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPolling",
                table: "Disks",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PollingInterval",
                table: "Disks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
