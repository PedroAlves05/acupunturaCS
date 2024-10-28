using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewAcupuntura.Migrations
{
    /// <inheritdoc />
    public partial class teste7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Atendimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MoraComQuem = table.Column<string>(type: "TEXT", nullable: true),
                    Peso = table.Column<double>(type: "REAL", nullable: false),
                    Altura = table.Column<double>(type: "REAL", nullable: false),
                    Filhos = table.Column<bool>(type: "INTEGER", nullable: false),
                    Fumante = table.Column<bool>(type: "INTEGER", nullable: false),
                    Vegetariano = table.Column<bool>(type: "INTEGER", nullable: false),
                    Alcool = table.Column<bool>(type: "INTEGER", nullable: false),
                    Drogas = table.Column<bool>(type: "INTEGER", nullable: false),
                    AtividadeFisica = table.Column<bool>(type: "INTEGER", nullable: false),
                    Meditacao = table.Column<bool>(type: "INTEGER", nullable: false),
                    PressaoAlta = table.Column<bool>(type: "INTEGER", nullable: false),
                    PressaoBaixa = table.Column<bool>(type: "INTEGER", nullable: false),
                    Queixa = table.Column<string>(type: "TEXT", nullable: true),
                    Doenca = table.Column<string>(type: "TEXT", nullable: true),
                    TipoDeTratamento = table.Column<string>(type: "TEXT", nullable: true),
                    QuantidadeSessoes = table.Column<int>(type: "INTEGER", nullable: false),
                    MetodoDePagamento = table.Column<string>(type: "TEXT", nullable: true),
                    Concordo = table.Column<bool>(type: "INTEGER", nullable: false),
                    Olho = table.Column<bool>(type: "INTEGER", nullable: false),
                    Olfato = table.Column<bool>(type: "INTEGER", nullable: false),
                    Mandibular = table.Column<bool>(type: "INTEGER", nullable: false),
                    Pulmoes = table.Column<bool>(type: "INTEGER", nullable: false),
                    Auditivo = table.Column<bool>(type: "INTEGER", nullable: false),
                    Estomago = table.Column<bool>(type: "INTEGER", nullable: false),
                    Garganta = table.Column<bool>(type: "INTEGER", nullable: false),
                    Gonadas = table.Column<bool>(type: "INTEGER", nullable: false),
                    Pancreas = table.Column<bool>(type: "INTEGER", nullable: false),
                    Coracao = table.Column<bool>(type: "INTEGER", nullable: false),
                    Figado = table.Column<bool>(type: "INTEGER", nullable: false),
                    Retal = table.Column<bool>(type: "INTEGER", nullable: false),
                    Ciatico = table.Column<bool>(type: "INTEGER", nullable: false),
                    Joelho = table.Column<bool>(type: "INTEGER", nullable: false),
                    Rim = table.Column<bool>(type: "INTEGER", nullable: false),
                    Trigemios = table.Column<bool>(type: "INTEGER", nullable: false),
                    Agressividade = table.Column<bool>(type: "INTEGER", nullable: false),
                    Tragus = table.Column<bool>(type: "INTEGER", nullable: false),
                    Pele = table.Column<bool>(type: "INTEGER", nullable: false),
                    Ombro = table.Column<bool>(type: "INTEGER", nullable: false),
                    MembrosInferiores = table.Column<bool>(type: "INTEGER", nullable: false),
                    MembrosSuperiores = table.Column<bool>(type: "INTEGER", nullable: false),
                    Alergia = table.Column<bool>(type: "INTEGER", nullable: false),
                    Darwin = table.Column<bool>(type: "INTEGER", nullable: false),
                    Sintase = table.Column<bool>(type: "INTEGER", nullable: false),
                    Talamo = table.Column<bool>(type: "INTEGER", nullable: false),
                    Occipital = table.Column<bool>(type: "INTEGER", nullable: false),
                    Genital = table.Column<bool>(type: "INTEGER", nullable: false),
                    Medular = table.Column<bool>(type: "INTEGER", nullable: false),
                    Observacao = table.Column<string>(type: "TEXT", nullable: true),
                    ConsultaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Consultas_ConsultaId",
                        column: x => x.ConsultaId,
                        principalTable: "Consultas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_ConsultaId",
                table: "Atendimentos",
                column: "ConsultaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atendimentos");
        }
    }
}
