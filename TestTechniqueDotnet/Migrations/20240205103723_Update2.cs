using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTechniqueDotnet.Migrations
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Clients_TransactionId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_TransactionId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Animals");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_MasterId",
                table: "Animals",
                column: "MasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Clients_MasterId",
                table: "Animals",
                column: "MasterId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Clients_MasterId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_MasterId",
                table: "Animals");

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId",
                table: "Animals",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_TransactionId",
                table: "Animals",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Clients_TransactionId",
                table: "Animals",
                column: "TransactionId",
                principalTable: "Clients",
                principalColumn: "Id");
        }
    }
}
