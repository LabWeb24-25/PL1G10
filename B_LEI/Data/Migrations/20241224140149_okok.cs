using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace B_LEI.Data.Migrations
{
    /// <inheritdoc />
    public partial class okok : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c87fc19-ba86-4a21-9298-abe1060f0219");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7595a85-5378-4e62-947a-d9eff9eb2833");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c9db5060-110c-42cf-a87e-9b1c24782922");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5030317e-ba4d-418a-a1f0-ddacaa679735", null, "leitor", "LEITOR" },
                    { "cb9c5106-3a31-4623-809b-34291437afbb", null, "bibliotecario", "BIBLIOTECARIO" },
                    { "e47f57ff-4ef7-4e16-a0e2-48452893ec22", null, "admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "Livros",
                keyColumn: "LivroId",
                keyValue: 1,
                column: "Capa",
                value: "C:\\Users\\Daniel\\source\\repos\\LabWeb24-25\\PL1G10\\B_LEI\\wwwroot\\images\\Senhor Dos Aneis.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5030317e-ba4d-418a-a1f0-ddacaa679735");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb9c5106-3a31-4623-809b-34291437afbb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e47f57ff-4ef7-4e16-a0e2-48452893ec22");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8c87fc19-ba86-4a21-9298-abe1060f0219", null, "bibliotecario", "BIBLIOTECARIO" },
                    { "c7595a85-5378-4e62-947a-d9eff9eb2833", null, "admin", "ADMIN" },
                    { "c9db5060-110c-42cf-a87e-9b1c24782922", null, "leitor", "LEITOR" }
                });

            migrationBuilder.UpdateData(
                table: "Livros",
                keyColumn: "LivroId",
                keyValue: 1,
                column: "Capa",
                value: "");
        }
    }
}
