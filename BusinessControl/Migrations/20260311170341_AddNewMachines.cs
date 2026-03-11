using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessControlService.Migrations
{
    /// <inheritdoc />
    public partial class AddNewMachines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MachineType",
                table: "MachineJobs",
                newName: "MachineId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "FieldJobs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStart",
                table: "FieldJobs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "FieldArea",
                table: "FieldJobs",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Latitude",
                table: "FieldJobs",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Longitude",
                table: "FieldJobs",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "MachineId",
                table: "FieldJobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineType = table.Column<int>(type: "int", nullable: false),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MachineSimpleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcquisitionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DecomissionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MachineJobs_MachineId",
                table: "MachineJobs",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldJobs_MachineId",
                table: "FieldJobs",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldJobs_Machines_MachineId",
                table: "FieldJobs",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineJobs_Machines_MachineId",
                table: "MachineJobs",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldJobs_Machines_MachineId",
                table: "FieldJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineJobs_Machines_MachineId",
                table: "MachineJobs");

            migrationBuilder.DropTable(
                name: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_MachineJobs_MachineId",
                table: "MachineJobs");

            migrationBuilder.DropIndex(
                name: "IX_FieldJobs_MachineId",
                table: "FieldJobs");

            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "FieldJobs");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "FieldJobs");

            migrationBuilder.DropColumn(
                name: "FieldArea",
                table: "FieldJobs");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "FieldJobs");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "FieldJobs");

            migrationBuilder.DropColumn(
                name: "MachineId",
                table: "FieldJobs");

            migrationBuilder.RenameColumn(
                name: "MachineId",
                table: "MachineJobs",
                newName: "MachineType");
        }
    }
}
