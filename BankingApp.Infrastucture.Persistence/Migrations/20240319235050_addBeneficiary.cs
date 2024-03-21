using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApp.Infrastucture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addBeneficiary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiary_Clients_ClientId",
                table: "Beneficiary");

            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiary_SavingsAccounts_SavingsAccountId",
                table: "Beneficiary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Beneficiary",
                table: "Beneficiary");

            migrationBuilder.RenameTable(
                name: "Beneficiary",
                newName: "Beneficiaries");

            migrationBuilder.RenameIndex(
                name: "IX_Beneficiary_SavingsAccountId",
                table: "Beneficiaries",
                newName: "IX_Beneficiaries_SavingsAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Beneficiary_ClientId",
                table: "Beneficiaries",
                newName: "IX_Beneficiaries_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Beneficiaries",
                table: "Beneficiaries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiaries_Clients_ClientId",
                table: "Beneficiaries",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiaries_SavingsAccounts_SavingsAccountId",
                table: "Beneficiaries",
                column: "SavingsAccountId",
                principalTable: "SavingsAccounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiaries_Clients_ClientId",
                table: "Beneficiaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiaries_SavingsAccounts_SavingsAccountId",
                table: "Beneficiaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Beneficiaries",
                table: "Beneficiaries");

            migrationBuilder.RenameTable(
                name: "Beneficiaries",
                newName: "Beneficiary");

            migrationBuilder.RenameIndex(
                name: "IX_Beneficiaries_SavingsAccountId",
                table: "Beneficiary",
                newName: "IX_Beneficiary_SavingsAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Beneficiaries_ClientId",
                table: "Beneficiary",
                newName: "IX_Beneficiary_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Beneficiary",
                table: "Beneficiary",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiary_Clients_ClientId",
                table: "Beneficiary",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiary_SavingsAccounts_SavingsAccountId",
                table: "Beneficiary",
                column: "SavingsAccountId",
                principalTable: "SavingsAccounts",
                principalColumn: "Id");
        }
    }
}
