using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace ITech.Migrations
{
    public partial class SeedUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var filePath = Path.Combine("SQLScripts/RolesWithUsers/dbo.AspNetUserRoles.Table.sql");
            migrationBuilder.Sql(File.ReadAllText(filePath));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var query = $@"DELETE FROM dbo.AspNetUserRoles
                           WHERE (UserId = N'e58fd1dd-a2dc-48c8-95b3-0317e7dbe372' AND RoleId = N'5ff13225-040b-441b-830a-c52e42a63ccc')
                           OR (UserId = N'5a4abd76-5c04-4351-a928-41eff4073f8e' AND RoleId = N'626a7c07-91c2-4ebd-b8c4-78a507e316aa')
                           OR (UserId = N'66c7c4af-9652-44a5-8ddc-785879439e4d' AND RoleId = N'626a7c07-91c2-4ebd-b8c4-78a507e316aa')
                           OR (UserId = N'a472710e-c805-4d97-8851-86df8e7c16e2' AND RoleId = N'626a7c07-91c2-4ebd-b8c4-78a507e316aa')
                           OR (UserId = N'849e4560-5ce3-490b-b15c-a57f626e93ba' AND RoleId = N'93860cdd-8df3-475c-9337-1e2b29911be4')
                           OR (UserId = N'0a6fad35-33fd-413c-97bf-30fa72862bd2' AND RoleId = N'a96f1582-05e7-4b4c-88b7-4beaaaa5145b')
                           OR (UserId = N'cfc507e5-0d9d-4061-8c61-517ef43f423f' AND RoleId = N'a96f1582-05e7-4b4c-88b7-4beaaaa5145b') ; ";
            migrationBuilder.Sql(query);
        }
    }
}
