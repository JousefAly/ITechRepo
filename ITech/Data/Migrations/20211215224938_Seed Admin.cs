using Microsoft.EntityFrameworkCore.Migrations;

namespace ITech.Data.Migrations
{
    public partial class SeedAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Discriminator], [FirstName], [LastName], [ProfilePicture]) VALUES (N'e58fd1dd-a2dc-48c8-95b3-0317e7dbe372', N'admin', N'ADMIN', N'admin@ITech.com', N'ADMIN@ITECH.COM', 0, N'AQAAAAEAACcQAAAAEFljr7VhIFbcuavIzbD3VTKvoepsg3rXpUHK9Ge4NBi0E93juT6syn/TU5HvJ7xNvQ==', N'XIING54HDYE7EWEG3JEXOP6MN6OGROV5', N'4116ee2e-13ba-4246-b198-e686dc2458be', N'01111111111', 0, 0, NULL, 1, 0, N'AppUser', N'adminFirstName', N'adminLastName', NULL)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUsers] WHERE Id ='N'e58fd1dd-a2dc-48c8-95b3-0317e7dbe372''");
        }
    }
}
