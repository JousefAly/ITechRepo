using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace ITech.Migrations
{
    public partial class SeedProductImagesScript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlPath = Path.Combine("SQLScripts/Products/dbo.ProductImages.Table.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlPath));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete FROM dbo.ProductImages WHERE Id > 500 AND Id < 1728;");
        }
    }
}
