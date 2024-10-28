using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewAcupuntura.Migrations
{
    /// <inheritdoc />
    public partial class teste9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "data",
                table: "Atendimentos",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data",
                table: "Atendimentos");
        }
    }
}
