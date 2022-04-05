using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalRestApi.Migrations
{
    public partial class RefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    token = table.Column<string>(type: "TEXT", nullable: true),
                    userId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");
        }
    }
}
