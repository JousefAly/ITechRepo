using Microsoft.EntityFrameworkCore.Migrations;

namespace ITech.Data.Migrations
{
    public partial class AssignAdminUserToAdminRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUserRoles] SELECT 'e58fd1dd-a2dc-48c8-95b3-0317e7dbe372', Id FROM[dbo].[AspNetRoles] WHERE Name = N'Admin';");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUserRoles] " +
                "WHERE UserId = 'e58fd1dd-a2dc-48c8-95b3-0317e7dbe372'" +
                "AND RoleId = 'f530a734-9b24-494c-8f4e-f3bd29babf41'");
        }
    }
}
