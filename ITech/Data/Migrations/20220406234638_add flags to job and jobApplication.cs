using Microsoft.EntityFrameworkCore.Migrations;

namespace ITech.Data.Migrations
{
    public partial class addflagstojobandjobApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "JobApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "JobApplications");
        }
    }
}
