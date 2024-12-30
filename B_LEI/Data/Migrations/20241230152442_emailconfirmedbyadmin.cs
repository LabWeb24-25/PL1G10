using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B_LEI.Data.Migrations
{
    /// <inheritdoc />
    public partial class emailconfirmedbyadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEmailConfirmedByAdmin",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmailConfirmedByAdmin",
                table: "AspNetUsers");
        }
    }
}
