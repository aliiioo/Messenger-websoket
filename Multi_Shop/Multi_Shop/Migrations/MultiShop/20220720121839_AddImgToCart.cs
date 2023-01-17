using Microsoft.EntityFrameworkCore.Migrations;

namespace Multi_Shop.Migrations.MultiShop
{
    public partial class AddImgToCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgName",
                table: "CartI",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgName",
                table: "CartI");
        }
    }
}
