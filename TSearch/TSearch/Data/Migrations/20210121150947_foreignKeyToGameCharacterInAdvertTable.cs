using Microsoft.EntityFrameworkCore.Migrations;

namespace TSearch.Data.Migrations
{
    public partial class foreignKeyToGameCharacterInAdvertTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CharacterLevel",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "CharacterName",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "ServerName",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "Vocation",
                table: "Adverts");

            migrationBuilder.AddColumn<int>(
                name: "GameCharacterId",
                table: "Adverts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Adverts_GameCharacterId",
                table: "Adverts",
                column: "GameCharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_Characters_GameCharacterId",
                table: "Adverts",
                column: "GameCharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_Characters_GameCharacterId",
                table: "Adverts");

            migrationBuilder.DropIndex(
                name: "IX_Adverts_GameCharacterId",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "GameCharacterId",
                table: "Adverts");

            migrationBuilder.AddColumn<int>(
                name: "CharacterLevel",
                table: "Adverts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CharacterName",
                table: "Adverts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServerName",
                table: "Adverts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Vocation",
                table: "Adverts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
