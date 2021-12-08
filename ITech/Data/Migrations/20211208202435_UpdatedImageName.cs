using Microsoft.EntityFrameworkCore.Migrations;

namespace ITech.Data.Migrations
{
    public partial class UpdatedImageName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image8Url",
                table: "Products",
                newName: "Image8Name");

            migrationBuilder.RenameColumn(
                name: "Image7Url",
                table: "Products",
                newName: "Image7Name");

            migrationBuilder.RenameColumn(
                name: "Image6Url",
                table: "Products",
                newName: "Image6Name");

            migrationBuilder.RenameColumn(
                name: "Image5Url",
                table: "Products",
                newName: "Image5Name");

            migrationBuilder.RenameColumn(
                name: "Image4Url",
                table: "Products",
                newName: "Image4Name");

            migrationBuilder.RenameColumn(
                name: "Image3Url",
                table: "Products",
                newName: "Image3Name");

            migrationBuilder.RenameColumn(
                name: "Image2Url",
                table: "Products",
                newName: "Image2Name");

            migrationBuilder.RenameColumn(
                name: "Image1Url",
                table: "Products",
                newName: "Image1Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image8Name",
                table: "Products",
                newName: "Image8Url");

            migrationBuilder.RenameColumn(
                name: "Image7Name",
                table: "Products",
                newName: "Image7Url");

            migrationBuilder.RenameColumn(
                name: "Image6Name",
                table: "Products",
                newName: "Image6Url");

            migrationBuilder.RenameColumn(
                name: "Image5Name",
                table: "Products",
                newName: "Image5Url");

            migrationBuilder.RenameColumn(
                name: "Image4Name",
                table: "Products",
                newName: "Image4Url");

            migrationBuilder.RenameColumn(
                name: "Image3Name",
                table: "Products",
                newName: "Image3Url");

            migrationBuilder.RenameColumn(
                name: "Image2Name",
                table: "Products",
                newName: "Image2Url");

            migrationBuilder.RenameColumn(
                name: "Image1Name",
                table: "Products",
                newName: "Image1Url");
        }
    }
}
