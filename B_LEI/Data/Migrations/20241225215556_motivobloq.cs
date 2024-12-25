using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B_LEI.Data.Migrations
{
    /// <inheritdoc />
    public partial class motivobloq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LockoutReason",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LockoutReason",
                table: "AspNetUsers");
        }
    }
}
