using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class Morir : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Boss_BossCode",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Boss");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BossCode",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BossCode",
                table: "Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BossCode",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Boss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OfficeCode = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Extention = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boss_offices_OfficeCode",
                        column: x => x.OfficeCode,
                        principalTable: "offices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Boss_persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BossCode",
                table: "Employees",
                column: "BossCode");

            migrationBuilder.CreateIndex(
                name: "IX_Boss_OfficeCode",
                table: "Boss",
                column: "OfficeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Boss_PersonId",
                table: "Boss",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Boss_BossCode",
                table: "Employees",
                column: "BossCode",
                principalTable: "Boss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
