using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class Pay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Clients_Client",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_MethodPayments_MethodId",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_clients_ClientCodeNavigationId",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_payments_ClientCodeNavigationId",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "ClientCodeNavigationId",
                table: "payments");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_MethodId",
                table: "payments",
                newName: "IX_payments_MethodId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "payments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_payments_ClienteId",
                table: "payments",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_payments_clients_ClienteId",
                table: "payments",
                column: "ClienteId",
                principalTable: "clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_payments_methodpayments_MethodId",
                table: "payments",
                column: "MethodId",
                principalTable: "methodpayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payments_clients_ClienteId",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_methodpayments_MethodId",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_payments_ClienteId",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "payments");

            migrationBuilder.RenameIndex(
                name: "IX_payments_MethodId",
                table: "payments",
                newName: "IX_Payments_MethodId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "payments",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "ClientCodeNavigationId",
                table: "payments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_payments_ClientCodeNavigationId",
                table: "payments",
                column: "ClientCodeNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Clients_Client",
                table: "payments",
                column: "Id",
                principalTable: "clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_MethodPayments_MethodId",
                table: "payments",
                column: "MethodId",
                principalTable: "methodpayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_clients_ClientCodeNavigationId",
                table: "payments",
                column: "ClientCodeNavigationId",
                principalTable: "clients",
                principalColumn: "Id");
        }
    }
}