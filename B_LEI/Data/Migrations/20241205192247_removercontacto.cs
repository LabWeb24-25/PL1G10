using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace B_LEI.Data.Migrations
{
    /// <inheritdoc />
#pragma warning disable CS8981 // O nome do tipo contém apenas caracteres ascii em caixa baixa. Esses nomes podem ficar reservados para o idioma.
    public partial class removercontacto : Migration
#pragma warning restore CS8981 // O nome do tipo contém apenas caracteres ascii em caixa baixa. Esses nomes podem ficar reservados para o idioma.
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32417a84-6228-430d-873a-64451883e7ee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8399a8c3-2b11-45c8-9caf-d46d99c099d0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3ee67d6-f3c5-4ced-ad86-ddd057bf344e");

            migrationBuilder.DropColumn(
                name: "Contacto",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "372adf98-7c3d-4cbe-9e33-bf1fe80c6a34", null, "bibliotecario", "bibliotecario" },
                    { "5c45d308-dcb8-42f2-9927-a0e250d1ff2c", null, "admin", "admin" },
                    { "a07ee143-6514-432a-9636-50ff5b4af7ac", null, "leitor", "leitor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "372adf98-7c3d-4cbe-9e33-bf1fe80c6a34");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c45d308-dcb8-42f2-9927-a0e250d1ff2c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a07ee143-6514-432a-9636-50ff5b4af7ac");

            migrationBuilder.AddColumn<string>(
                name: "Contacto",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "32417a84-6228-430d-873a-64451883e7ee", null, "admin", "admin" },
                    { "8399a8c3-2b11-45c8-9caf-d46d99c099d0", null, "bibliotecario", "bibliotecario" },
                    { "c3ee67d6-f3c5-4ced-ad86-ddd057bf344e", null, "leitor", "leitor" }
                });
        }
    }
}
