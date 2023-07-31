using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Online_Shop.Migrations
{
    public partial class migracija8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "TableUsers");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "TableUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "TableUsers");

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "TableUsers",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
