using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarhabilitacionaCategoriasySubCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Habilitado",
                table: "SubCategoria",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Habilitado",
                table: "Categoria",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Habilitado",
                table: "SubCategoria");

            migrationBuilder.DropColumn(
                name: "Habilitado",
                table: "Categoria");
        }
    }
}
