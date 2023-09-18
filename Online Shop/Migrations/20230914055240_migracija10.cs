using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Online_Shop.Migrations
{
    public partial class migracija10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Picture",
                table: "TableProducts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KorisnikID",
                table: "TableProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TableProducts_KorisnikID",
                table: "TableProducts",
                column: "KorisnikID");

            migrationBuilder.AddForeignKey(
                name: "FK_TableProducts_TableUsers_KorisnikID",
                table: "TableProducts",
                column: "KorisnikID",
                principalTable: "TableUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableProducts_TableUsers_KorisnikID",
                table: "TableProducts");

            migrationBuilder.DropIndex(
                name: "IX_TableProducts_KorisnikID",
                table: "TableProducts");

            migrationBuilder.DropColumn(
                name: "KorisnikID",
                table: "TableProducts");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Picture",
                table: "TableProducts",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
