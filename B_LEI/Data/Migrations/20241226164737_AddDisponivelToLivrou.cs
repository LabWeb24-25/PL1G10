using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B_LEI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDisponivelToLivrou : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Livros",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Livros");
        }
    }
}
