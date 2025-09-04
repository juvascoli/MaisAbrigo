using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaisAbrigo.Migrations
{
    public static class OracleTypes
    {
        public const string Number10 = "NUMBER(10)";
        public const string NVarChar100 = "NVARCHAR2(100)";
        public const string NVarChar200 = "NVARCHAR2(200)";
    }

    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abrigos",
                columns: table => new
                {
                    Id = table.Column<int>(type: OracleTypes.Number10, nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: OracleTypes.NVarChar100, maxLength: 100, nullable: false),
                    Endereco = table.Column<string>(type: OracleTypes.NVarChar200, maxLength: 200, nullable: false),
                    OcupacaoAtual = table.Column<int>(type: OracleTypes.Number10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abrigos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<int>(type: OracleTypes.Number10, nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: OracleTypes.NVarChar100, maxLength: 100, nullable: false),
                    Idade = table.Column<int>(type: OracleTypes.Number10, nullable: false),
                    sexo = table.Column<string>(type: OracleTypes.NVarChar100, maxLength: 100, nullable: false),
                    IdAbrigo = table.Column<int>(type: OracleTypes.Number10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pessoas_Abrigos_IdAbrigo",
                        column: x => x.IdAbrigo,
                        principalTable: "Abrigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_IdAbrigo",
                table: "Pessoas",
                column: "IdAbrigo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.DropTable(
                name: "Abrigos");
        }
    }
}
