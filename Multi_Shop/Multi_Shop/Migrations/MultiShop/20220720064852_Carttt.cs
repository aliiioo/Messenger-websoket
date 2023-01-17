using Microsoft.EntityFrameworkCore.Migrations;

namespace Multi_Shop.Migrations.MultiShop
{
    public partial class Carttt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartI",
                columns: table => new
                {
                    CarttId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Sizing = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Coloring = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Numbers = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartI", x => x.CarttId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartI");
        }
    }
}
