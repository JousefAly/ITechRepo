using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace ITech.Migrations
{
    public partial class SeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var filePath = Path.Combine("SQLScripts/RolesWithUsers/dbo.AspNetUsers.Table.sql");
            migrationBuilder.Sql(File.ReadAllText(filePath));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var query = $@"DELETE FROM dbo.AspNetUsers
                           WHERE Id = N'0a6fad35-33fd-413c-97bf-30fa72862bd2'
                           OR Id = N'5a4abd76-5c04-4351-a928-41eff4073f8e'
                           OR Id = N'66c7c4af-9652-44a5-8ddc-785879439e4d'
                           OR Id = N'849e4560-5ce3-490b-b15c-a57f626e93ba'
                           OR Id = N'a472710e-c805-4d97-8851-86df8e7c16e2'
                           OR Id = N'b8f78381-86e2-4cc3-912e-a9bbc442d0db'
                           OR Id = N'cfc507e5-0d9d-4061-8c61-517ef43f423f'
                           OR Id = N'e58fd1dd-a2dc-48c8-95b3-0317e7dbe372' ; ";
            migrationBuilder.Sql(query);
        }
    }
}
