using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace ITech.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var filePath = Path.Combine("SQLScripts/RolesWithUsers/dbo.AspNetRoles.Table.sql");
            migrationBuilder.Sql(File.ReadAllText(filePath));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var query = $@"DELETE FROM dbo.AspNetRoles
                           WHERE Id = N'5ff13225-040b-441b-830a-c52e42a63ccc'
                           OR Id = N'626a7c07-91c2-4ebd-b8c4-78a507e316aa'
                           OR Id = N'93860cdd-8df3-475c-9337-1e2b29911be4'
                           OR Id = N'a96f1582-05e7-4b4c-88b7-4beaaaa5145b'; ";
            migrationBuilder.Sql(query);
        }
    }
}
