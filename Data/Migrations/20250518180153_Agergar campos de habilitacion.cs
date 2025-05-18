using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Agergarcamposdehabilitacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Habilitado",
                table: "Vehiculos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Habilitado",
                table: "Timbrados",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Habilitado",
                table: "ModeloVehiculos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Habilitado",
                table: "MarcaVehiculos",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Habilitado",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "Habilitado",
                table: "Timbrados");

            migrationBuilder.DropColumn(
                name: "Habilitado",
                table: "ModeloVehiculos");

            migrationBuilder.DropColumn(
                name: "Habilitado",
                table: "MarcaVehiculos");
        }
    }
}
