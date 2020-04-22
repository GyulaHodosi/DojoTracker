using Microsoft.EntityFrameworkCore.Migrations;

namespace DojoTracker.Migrations
{
    public partial class AddUrlColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Dojos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Dojos");
        }
    }
}
