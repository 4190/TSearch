using Microsoft.EntityFrameworkCore.Migrations;

namespace TSearch.Data.Migrations
{
    public partial class AppUserIdForeignKeyInAdvertsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Adverts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Adverts_ApplicationUserId",
                table: "Adverts",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_AspNetUsers_ApplicationUserId",
                table: "Adverts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_AspNetUsers_ApplicationUserId",
                table: "Adverts");

            migrationBuilder.DropIndex(
                name: "IX_Adverts_ApplicationUserId",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Adverts");
        }
    }
}
