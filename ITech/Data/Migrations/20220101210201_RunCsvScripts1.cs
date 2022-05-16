using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace ITech.Data.Migrations
{
    public partial class RunCsvScripts1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //seed categories
            var sqlFileCategories = Path.Combine("SQLScripts/CategoriesScript1.Sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFileCategories));
            //apply scripts
            var sqlFileProducts = Path.Combine("SQLScripts/ProductsScript1.Sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFileProducts));
            var sqlFileProductDetails = Path.Combine("SQLScripts/ProductDetailsScript1.Sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFileProductDetails));
            var sqlFileProductImages = Path.Combine("SQLScripts/ProductImagesScript1.Sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFileProductImages));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categories WHERE Id < 3");
            migrationBuilder.Sql("DELETE FROM Products WHERE Id > 499");
            migrationBuilder.Sql("DELETE FROM ProductDetails WHERE ProductId > 499");
            migrationBuilder.Sql("DELETE FROM ProductImage WHERE ProductId > 499");
        }
    }
}
