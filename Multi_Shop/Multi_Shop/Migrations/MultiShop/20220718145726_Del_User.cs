using Microsoft.EntityFrameworkCore.Migrations;

namespace Multi_Shop.Migrations.MultiShop
{
    public partial class Del_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Usersss_UserId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_UserId",
                table: "Likes");

            migrationBuilder.AddColumn<int>(
                name: "UserListidd",
                table: "Likes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserListidd",
                table: "Likes",
                column: "UserListidd");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Usersss_UserListidd",
                table: "Likes",
                column: "UserListidd",
                principalTable: "Usersss",
                principalColumn: "idd",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Usersss_UserListidd",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_UserListidd",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "UserListidd",
                table: "Likes");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Usersss_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "Usersss",
                principalColumn: "idd",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
