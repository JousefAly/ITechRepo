using Microsoft.EntityFrameworkCore.Migrations;

namespace ITech.Data.Migrations
{
    public partial class AddedSeedsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image4Url",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image5Url",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image6Url",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image7Url",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image8Url",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Seeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfSeedType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesiredSeed = table.Column<int>(type: "int", nullable: false),
                    Seeded = table.Column<bool>(type: "bit", nullable: false),
                    SeedAttempts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seeds", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Seeds");

            migrationBuilder.DropColumn(
                name: "Image4Url",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image5Url",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image6Url",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image7Url",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image8Url",
                table: "Products");
        }
    }
}
