using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace ITech.Migrations
{
    public partial class SeedProductDetailsScript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlPath = Path.Combine("SQLScripts/Products/dbo.ProductDetails.Table.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlPath));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete FROM dbo.ProductDetails WHERE Id > 500 AND Id < 907;");
        }
    }
}
