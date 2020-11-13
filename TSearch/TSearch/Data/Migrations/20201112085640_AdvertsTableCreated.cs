using Microsoft.EntityFrameworkCore.Migrations;

namespace TSearch.Data.Migrations
{
    public partial class AdvertsTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adverts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorName = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    CharacterName = table.Column<string>(nullable: false),
                    ServerName = table.Column<string>(nullable: false),
                    Vocation = table.Column<string>(nullable: false),
                    MinLevel = table.Column<int>(nullable: false),
                    MaxLevel = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adverts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adverts");
        }
    }
}
