using Microsoft.EntityFrameworkCore.Migrations;

namespace ITech.Data.Migrations
{
    public partial class OrderHasHandlerAndUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HandlerId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_HandlerId",
                table: "Orders",
                column: "HandlerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_HandlerId",
                table: "Orders",
                column: "HandlerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_HandlerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_HandlerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "HandlerId",
                table: "Orders");
        }
    }
}
