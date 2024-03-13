using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class PropertiesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                schema: "Identity",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "IdentificationNumber",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Identity",
                table: "Users",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentificationNumber",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Identity",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
