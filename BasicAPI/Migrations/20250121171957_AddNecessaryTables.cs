using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BasicAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNecessaryTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Licenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_FORNECEDORES",
                columns: table => new
                {
                    FOR_CODIGO = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FOR_DESCRICAO = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_FORNECEDORES", x => x.FOR_CODIGO);
                });

            migrationBuilder.CreateTable(
                name: "TB_FUNCIONARIOS",
                columns: table => new
                {
                    FUN_CODIGO = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FUN_NOME = table.Column<string>(type: "text", nullable: false),
                    FUN_CPF = table.Column<string>(type: "text", nullable: false),
                    FUN_SENHA = table.Column<string>(type: "text", nullable: false),
                    FUN_FUNCAO = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_FUNCIONARIOS", x => x.FUN_CODIGO);
                });

            migrationBuilder.CreateTable(
                name: "TB_ITENS",
                columns: table => new
                {
                    ITE_CODIGO = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ITE_QUANTIDADE = table.Column<decimal>(type: "numeric", nullable: false),
                    ITE_VALOR_PRODUTOS = table.Column<decimal>(type: "numeric", nullable: false),
                    TB_PRODUTOS_PRO_CODIGO = table.Column<long>(type: "bigint", nullable: false),
                    TB_VENDAS_VEN_CODIGO = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ITENS", x => x.ITE_CODIGO);
                });

            migrationBuilder.CreateTable(
                name: "TB_PRODUTOS",
                columns: table => new
                {
                    PRO_CODIGO = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PRO_DESCRICAO = table.Column<string>(type: "text", nullable: false),
                    PRO_VALOR = table.Column<decimal>(type: "numeric", nullable: false),
                    PRO_QUANTIDADE = table.Column<decimal>(type: "numeric", nullable: false),
                    TB_FORNECEDORES_FOR_CODIGO = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PRODUTOS", x => x.PRO_CODIGO);
                });

            migrationBuilder.CreateTable(
                name: "TB_VENDAS",
                columns: table => new
                {
                    VEN_CODIGO = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VEN_HORARIO = table.Column<TimeSpan>(type: "interval", nullable: false),
                    VEN_VALOR_TOTAL = table.Column<decimal>(type: "numeric(7,2)", nullable: false),
                    TB_FUNCIONARIOS_FUN_CODIGO = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_VENDAS", x => x.VEN_CODIGO);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Licenses");

            migrationBuilder.DropTable(
                name: "TB_FORNECEDORES");

            migrationBuilder.DropTable(
                name: "TB_FUNCIONARIOS");

            migrationBuilder.DropTable(
                name: "TB_ITENS");

            migrationBuilder.DropTable(
                name: "TB_PRODUTOS");

            migrationBuilder.DropTable(
                name: "TB_VENDAS");
        }
    }
}
