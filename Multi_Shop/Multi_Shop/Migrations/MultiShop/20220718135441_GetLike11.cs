using Microsoft.EntityFrameworkCore.Migrations;

namespace Multi_Shop.Migrations.MultiShop
{
    public partial class GetLike11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Usersss_UserId",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usersss",
                table: "Usersss");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Usersss");

            migrationBuilder.AddColumn<int>(
                name: "idd",
                table: "Usersss",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Likes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usersss",
                table: "Usersss",
                column: "idd");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Usersss_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "Usersss",
                principalColumn: "idd",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Usersss_UserId",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usersss",
                table: "Usersss");

            migrationBuilder.DropColumn(
                name: "idd",
                table: "Usersss");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Usersss",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Likes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usersss",
                table: "Usersss",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Usersss_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "Usersss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
