using Microsoft.EntityFrameworkCore.Migrations;

namespace TSearch.Data.Migrations
{
    public partial class AddedOwnCharacterLevelFieldToAdvertModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CharacterLevel",
                table: "Adverts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CharacterLevel",
                table: "Adverts");
        }
    }
}
