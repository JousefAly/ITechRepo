using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace ITech.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlPath = Path.Combine("SQLScripts/Products/dbo.Products.Table.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlPath));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete FROM dbo.Products WHERE Id > 500 AND Id < 574;");
        }
    }
}
