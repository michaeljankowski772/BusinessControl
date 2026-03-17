using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessControlService.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "FieldAreaAccepted",
                table: "FieldJobs",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "PricePerArea",
                table: "FieldJobs",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "PricePerAreaAccepted",
                table: "FieldJobs",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldAreaAccepted",
                table: "FieldJobs");

            migrationBuilder.DropColumn(
                name: "PricePerArea",
                table: "FieldJobs");

            migrationBuilder.DropColumn(
                name: "PricePerAreaAccepted",
                table: "FieldJobs");
        }
    }
}
