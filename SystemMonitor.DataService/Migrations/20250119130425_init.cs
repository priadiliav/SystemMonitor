using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemMonitor.DataService.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComputerDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatingDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatingDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComputerMetrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Cpu = table.Column<double>(type: "double precision", nullable: false),
                    Ram = table.Column<double>(type: "double precision", nullable: false),
                    Disk = table.Column<double>(type: "double precision", nullable: false),
                    NetworkUsage = table.Column<double>(type: "double precision", nullable: false),
                    ComputerDetailsId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatingDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatingDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputerMetrics_ComputerDetails_ComputerDetailsId",
                        column: x => x.ComputerDetailsId,
                        principalTable: "ComputerDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComputerMetrics_ComputerDetailsId",
                table: "ComputerMetrics",
                column: "ComputerDetailsId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComputerMetrics");

            migrationBuilder.DropTable(
                name: "ComputerDetails");
        }
    }
}
