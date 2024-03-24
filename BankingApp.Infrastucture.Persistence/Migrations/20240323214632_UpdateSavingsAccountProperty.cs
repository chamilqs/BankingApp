using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Infrastucture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSavingsAccountProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isMainAccount",
                table: "SavingsAccounts",
                newName: "IsMainAccount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsMainAccount",
                table: "SavingsAccounts",
                newName: "isMainAccount");
        }
    }
}
