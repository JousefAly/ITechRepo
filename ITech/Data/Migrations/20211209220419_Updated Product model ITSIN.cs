using Microsoft.EntityFrameworkCore.Migrations;

namespace ITech.Data.Migrations
{
    public partial class UpdatedProductmodelITSIN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ITSIN",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ITSIN",
                table: "Products");
        }
    }
}
