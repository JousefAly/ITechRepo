using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace ITech.Migrations
{
    public partial class SeedSellers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var filePath = Path.Combine("SQLScripts/RolesWithUsers/dbo.Sellers.Table.sql");
            migrationBuilder.Sql(File.ReadAllText(filePath));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var query = $@"DELETE FROM dbo.Sellers
                           WHERE Id = N'5b821f3a-bfc1-4dab-b7b3-877c6abe647c'
                           OR Id = N'b40e2676-0ebc-41b1-918e-2ec535374760'
                           OR Id = N'fd3500a7-9809-40b1-9ada-9b3cd8138b9b' ; ";
            migrationBuilder.Sql(query);
        }
    }
}
