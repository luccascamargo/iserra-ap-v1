using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iserra_api.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoCampoImagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Imagen",
                table: "Users",
                newName: "Imagem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Imagem",
                table: "Users",
                newName: "Imagen");
        }
    }
}
