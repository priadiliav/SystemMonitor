using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemMonitor.DataService.Migrations
{
    /// <inheritdoc />
    public partial class Someupdateds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ram",
                table: "ComputerMetrics",
                newName: "RamUsage");

            migrationBuilder.RenameColumn(
                name: "Disk",
                table: "ComputerMetrics",
                newName: "DiskUsage");

            migrationBuilder.RenameColumn(
                name: "Cpu",
                table: "ComputerMetrics",
                newName: "CpuUsage");

            migrationBuilder.AlterColumn<long>(
                name: "NetworkUsage",
                table: "ComputerMetrics",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RamUsage",
                table: "ComputerMetrics",
                newName: "Ram");

            migrationBuilder.RenameColumn(
                name: "DiskUsage",
                table: "ComputerMetrics",
                newName: "Disk");

            migrationBuilder.RenameColumn(
                name: "CpuUsage",
                table: "ComputerMetrics",
                newName: "Cpu");

            migrationBuilder.AlterColumn<double>(
                name: "NetworkUsage",
                table: "ComputerMetrics",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
