using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace ITech.Migrations
{
    public partial class SeedCategoriesScript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlPath = Path.Combine("SQLScripts/Products/dbo.Categories.Table.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlPath));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete FROM dbo.Categories WHERE Id <= 2;");
        }
    }
}
