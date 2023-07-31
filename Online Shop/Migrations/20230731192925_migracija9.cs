using Microsoft.EntityFrameworkCore.Migrations;

namespace Online_Shop.Migrations
{
    public partial class migracija9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "TableUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "TableUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
