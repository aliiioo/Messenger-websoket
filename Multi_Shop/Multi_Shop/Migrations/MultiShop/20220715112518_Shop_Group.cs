using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Multi_Shop.Migrations.MultiShop
{
    public partial class Shop_Group : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopGroups_ShopGroups_ParentId",
                table: "ShopGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopGroups",
                table: "ShopGroups");

            migrationBuilder.RenameTable(
                name: "ShopGroups",
                newName: "ShopGroup");

            migrationBuilder.RenameIndex(
                name: "IX_ShopGroups_ParentId",
                table: "ShopGroup",
                newName: "IX_ShopGroup_ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopGroup",
                table: "ShopGroup",
                column: "GroupId");

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    ShopId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    ShopTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopImg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.ShopId);
                    table.ForeignKey(
                        name: "FK_Shops_ShopGroup_GroupId",
                        column: x => x.GroupId,
                        principalTable: "ShopGroup",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shops_GroupId",
                table: "Shops",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopGroup_ShopGroup_ParentId",
                table: "ShopGroup",
                column: "ParentId",
                principalTable: "ShopGroup",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopGroup_ShopGroup_ParentId",
                table: "ShopGroup");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopGroup",
                table: "ShopGroup");

            migrationBuilder.RenameTable(
                name: "ShopGroup",
                newName: "ShopGroups");

            migrationBuilder.RenameIndex(
                name: "IX_ShopGroup_ParentId",
                table: "ShopGroups",
                newName: "IX_ShopGroups_ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopGroups",
                table: "ShopGroups",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopGroups_ShopGroups_ParentId",
                table: "ShopGroups",
                column: "ParentId",
                principalTable: "ShopGroups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
