using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Integratte.Infra.Testes.Migrations
{
    public partial class TestesInfra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TestesInfra");

            migrationBuilder.CreateTable(
                name: "Clientes",
                schema: "TestesInfra",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Testes",
                schema: "TestesInfra",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientesDoTeste",
                schema: "TestesInfra",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CadastradoEm = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IdDoCliente = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdDoTeste = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientesDoTeste", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientesDoTeste_Clientes_IdDoCliente",
                        column: x => x.IdDoCliente,
                        principalSchema: "TestesInfra",
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientesDoTeste_Testes_IdDoTeste",
                        column: x => x.IdDoTeste,
                        principalSchema: "TestesInfra",
                        principalTable: "Testes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExemploChaveUnica",
                schema: "TestesInfra",
                table: "Clientes",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientesDoTeste_IdDoCliente",
                schema: "TestesInfra",
                table: "ClientesDoTeste",
                column: "IdDoCliente");

            migrationBuilder.CreateIndex(
                name: "IX_ClientesDoTeste_IdDoTeste",
                schema: "TestesInfra",
                table: "ClientesDoTeste",
                column: "IdDoTeste");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientesDoTeste",
                schema: "TestesInfra");

            migrationBuilder.DropTable(
                name: "Clientes",
                schema: "TestesInfra");

            migrationBuilder.DropTable(
                name: "Testes",
                schema: "TestesInfra");
        }
    }
}
