using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalRestApi.Migrations
{
    public partial class AddedMoreEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChassisType",
                table: "Vehicles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "Vehicles",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBathroomInside",
                table: "Vehicles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Vehicles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAllowedPeople",
                table: "Vehicles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAxis",
                table: "Vehicles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PricePerHour",
                table: "Vehicles",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationPlate",
                table: "Vehicles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Space",
                table: "Vehicles",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalLenght",
                table: "Vehicles",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "VinNumber",
                table: "Vehicles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<double>(
                name: "Width",
                table: "Vehicles",
                type: "REAL",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChassisType",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "IsBathroomInside",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "NumberOfAllowedPeople",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "NumberOfAxis",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "PricePerHour",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "RegistrationPlate",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Space",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "TotalLenght",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "VinNumber",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Vehicles");
        }
    }
}
