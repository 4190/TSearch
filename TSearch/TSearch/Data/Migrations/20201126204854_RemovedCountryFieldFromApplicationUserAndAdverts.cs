using Microsoft.EntityFrameworkCore.Migrations;

namespace TSearch.Data.Migrations
{
    public partial class RemovedCountryFieldFromApplicationUserAndAdverts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Adverts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Adverts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
