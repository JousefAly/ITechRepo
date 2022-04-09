using Microsoft.EntityFrameworkCore.Migrations;

namespace ITech.Data.Migrations
{
    public partial class UpdateAcceptedOrderDatelogic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AcceptedDate",
                table: "Orders",
                newName: "OrderHandeled");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderHandeled",
                table: "Orders",
                newName: "AcceptedDate");
        }
    }
}
