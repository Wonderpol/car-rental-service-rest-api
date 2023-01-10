using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalRestApi.Migrations
{
    public partial class NewMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransmissionType",
                table: "Vehicles");

            migrationBuilder.AddColumn<int>(
                name: "TransmissionTypeId",
                table: "Vehicles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TransmissionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransmissionTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_TransmissionTypeId",
                table: "Vehicles",
                column: "TransmissionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_TransmissionTypes_TransmissionTypeId",
                table: "Vehicles",
                column: "TransmissionTypeId",
                principalTable: "TransmissionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_TransmissionTypes_TransmissionTypeId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "TransmissionTypes");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_TransmissionTypeId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "TransmissionTypeId",
                table: "Vehicles");

            migrationBuilder.AddColumn<string>(
                name: "TransmissionType",
                table: "Vehicles",
                type: "TEXT",
                nullable: true);
        }
    }
}
