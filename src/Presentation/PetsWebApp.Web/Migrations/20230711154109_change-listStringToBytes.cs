using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetsWebApp.Web.Migrations
{
    public partial class changelistStringToBytes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "PetPhotos");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photos",
                table: "PetPhotos",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photos",
                table: "PetPhotos");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "PetPhotos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
