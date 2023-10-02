using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetsWebApp.Web.Migrations
{
    public partial class AddUserIdToPet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Pets",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pets");
        }
    }
}
