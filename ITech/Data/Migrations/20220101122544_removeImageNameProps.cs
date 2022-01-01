using Microsoft.EntityFrameworkCore.Migrations;

namespace ITech.Data.Migrations
{
    public partial class removeImageNameProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image1Name",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image2Name",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image3Name",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image4Name",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image5Name",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image6Name",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image7Name",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image8Name",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image1Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image2Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image3Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image4Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image5Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image6Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image7Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image8Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
