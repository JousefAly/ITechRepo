using Microsoft.EntityFrameworkCore.Migrations;

namespace ITech.Data.Migrations
{
    public partial class AddedSavedProductsRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUserProduct",
                columns: table => new
                {
                    SavedProductsId = table.Column<int>(type: "int", nullable: false),
                    SavingUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserProduct", x => new { x.SavedProductsId, x.SavingUsersId });
                    table.ForeignKey(
                        name: "FK_AppUserProduct_AspNetUsers_SavingUsersId",
                        column: x => x.SavingUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserProduct_Products_SavedProductsId",
                        column: x => x.SavedProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserProduct_SavingUsersId",
                table: "AppUserProduct",
                column: "SavingUsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserProduct");
        }
    }
}
