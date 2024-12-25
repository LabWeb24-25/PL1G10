using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace B_LEI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNomeToAspNetUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "450a4f12-cd82-41f4-848a-21f2c91f7f2b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6fa224f8-9e73-4f34-9153-2a7d9514050b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f86a927c-4d86-4f4a-8e2c-288151689fd6");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "450a4f12-cd82-41f4-848a-21f2c91f7f2b", null, "admin", "ADMIN" },
                    { "6fa224f8-9e73-4f34-9153-2a7d9514050b", null, "bibliotecario", "BIBLIOTECARIO" },
                    { "f86a927c-4d86-4f4a-8e2c-288151689fd6", null, "leitor", "LEITOR" }
                });
        }
    }
}
