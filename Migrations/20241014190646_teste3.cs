using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewAcupuntura.Migrations
{
    /// <inheritdoc />
    public partial class teste3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Usuarios_UsuarioId",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Consultas",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_AspNetUsers_UsuarioId",
                table: "Consultas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_AspNetUsers_UsuarioId",
                table: "Consultas");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Consultas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Usuarios_UsuarioId",
                table: "Consultas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
