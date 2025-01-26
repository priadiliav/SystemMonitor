using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemMonitor.DataService.Migrations
{
    /// <inheritdoc />
    public partial class made_new_test_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "ComputerMetrics",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "ComputerDetails",
                newName: "Status");

            migrationBuilder.AddColumn<Guid>(
                name: "ComputerMetricsId",
                table: "ComputerDetails",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ComputerDetails_Name",
                table: "ComputerDetails",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ComputerDetails_Name",
                table: "ComputerDetails");

            migrationBuilder.DropColumn(
                name: "ComputerMetricsId",
                table: "ComputerDetails");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "ComputerMetrics",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "ComputerDetails",
                newName: "status");
        }
    }
}
