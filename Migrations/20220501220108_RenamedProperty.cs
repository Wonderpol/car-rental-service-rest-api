using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalRestApi.Migrations
{
    public partial class RenamedProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hp",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "TotalLenght",
                table: "Vehicles",
                newName: "TotalLength");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalLength",
                table: "Vehicles",
                newName: "TotalLenght");

            migrationBuilder.AddColumn<int>(
                name: "Hp",
                table: "Vehicles",
                type: "INTEGER",
                nullable: true);
        }
    }
}
