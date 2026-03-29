using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessControlService.Migrations
{
    /// <inheritdoc />
    public partial class fieldjobnullableFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldJobs_Machines_MachineId",
                table: "FieldJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_FieldJobs_Workers_WorkerId",
                table: "FieldJobs");

            migrationBuilder.AlterColumn<int>(
                name: "WorkerId",
                table: "FieldJobs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MachineId",
                table: "FieldJobs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldJobs_Machines_MachineId",
                table: "FieldJobs",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldJobs_Workers_WorkerId",
                table: "FieldJobs",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldJobs_Machines_MachineId",
                table: "FieldJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_FieldJobs_Workers_WorkerId",
                table: "FieldJobs");

            migrationBuilder.AlterColumn<int>(
                name: "WorkerId",
                table: "FieldJobs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MachineId",
                table: "FieldJobs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FieldJobs_Machines_MachineId",
                table: "FieldJobs",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FieldJobs_Workers_WorkerId",
                table: "FieldJobs",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
