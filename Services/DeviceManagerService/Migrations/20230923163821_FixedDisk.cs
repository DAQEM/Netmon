using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeviceManagerService.Migrations
{
    /// <inheritdoc />
    public partial class FixedDisk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Disks",
                newName: "UsedSpace");

            migrationBuilder.RenameColumn(
                name: "Mount",
                table: "Disks",
                newName: "MountingPoint");

            migrationBuilder.RenameColumn(
                name: "AllocationUnit",
                table: "Disks",
                newName: "TotalSpace");

            migrationBuilder.RenameIndex(
                name: "IX_Disks_Mount_DeviceId",
                table: "Disks",
                newName: "IX_Disks_MountingPoint_DeviceId");

            migrationBuilder.AddColumn<long>(
                name: "AllocationUnits",
                table: "Disks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllocationUnits",
                table: "Disks");

            migrationBuilder.RenameColumn(
                name: "UsedSpace",
                table: "Disks",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "TotalSpace",
                table: "Disks",
                newName: "AllocationUnit");

            migrationBuilder.RenameColumn(
                name: "MountingPoint",
                table: "Disks",
                newName: "Mount");

            migrationBuilder.RenameIndex(
                name: "IX_Disks_MountingPoint_DeviceId",
                table: "Disks",
                newName: "IX_Disks_Mount_DeviceId");
        }
    }
}
