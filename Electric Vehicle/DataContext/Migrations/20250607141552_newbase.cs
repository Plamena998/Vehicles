using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataContext.Migrations
{
    /// <inheritdoc />
    public partial class newbase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CencusTractId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountyId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CencusTractId",
                table: "Vehicles",
                column: "CencusTractId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CityId",
                table: "Vehicles",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CountyId",
                table: "Vehicles",
                column: "CountyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_CencusTracts_CencusTractId",
                table: "Vehicles",
                column: "CencusTractId",
                principalTable: "CencusTracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Cities_CityId",
                table: "Vehicles",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Counties_CountyId",
                table: "Vehicles",
                column: "CountyId",
                principalTable: "Counties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_CencusTracts_CencusTractId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Cities_CityId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Counties_CountyId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_CencusTractId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_CityId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_CountyId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "CencusTractId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "CountyId",
                table: "Vehicles");
        }
    }
}
