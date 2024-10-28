using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewAcupuntura.Migrations
{
    /// <inheritdoc />
    public partial class teste8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Atendimentos_ConsultaId",
                table: "Atendimentos");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_ConsultaId",
                table: "Atendimentos",
                column: "ConsultaId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Atendimentos_ConsultaId",
                table: "Atendimentos");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_ConsultaId",
                table: "Atendimentos",
                column: "ConsultaId");
        }
    }
}
