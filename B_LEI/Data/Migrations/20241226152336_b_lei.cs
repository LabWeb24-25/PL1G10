using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B_LEI.Data.Migrations
{
    /// <inheritdoc />
    public partial class b_lei : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Livros",
                keyColumn: "LivroId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Autores",
                keyColumn: "AutorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Editoras",
                keyColumn: "EditoraId",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Autores",
                columns: new[] { "AutorId", "Foto", "LivroId", "Nome" },
                values: new object[] { 1, null, 0, "J.R.R. Tolkien" });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "CategoriaId", "Descricao", "LivroId", "Nome" },
                values: new object[] { 1, null, 0, "Ficção" });

            migrationBuilder.InsertData(
                table: "Editoras",
                columns: new[] { "EditoraId", "Descricao", "LivroId", "Nome" },
                values: new object[] { 1, null, 0, "HarperCollins" });

            migrationBuilder.InsertData(
                table: "Livros",
                columns: new[] { "LivroId", "AnoPublicacao", "AutorId", "Capa", "CategoriaId", "Descricao", "Edicao", "EditoraId", "ISBN", "Titulo" },
                values: new object[] { 1, 1954, 1, "/images/senhor_dos_aneis.png", 1, "Um clássico da fantasia.", "1ª", 1, "978-3-16-148410-0", "O Senhor dos Anéis" });
        }
    }
}
