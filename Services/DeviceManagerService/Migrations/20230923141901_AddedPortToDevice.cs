using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeviceManagerService.Migrations
{
    /// <inheritdoc />
    public partial class AddedPortToDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Port",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Port",
                table: "Devices");
        }
    }
}
