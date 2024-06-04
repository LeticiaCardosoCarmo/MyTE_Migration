using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTE_Migration.Migrations
{
    /// <inheritdoc />
    public partial class MigrationTres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HorasTrabalhadas_Funcionario_Funcionario_ID1",
                table: "HorasTrabalhadas");

            migrationBuilder.DropIndex(
                name: "IX_HorasTrabalhadas_Funcionario_ID1",
                table: "HorasTrabalhadas");

            migrationBuilder.DropColumn(
                name: "Funcionario_ID1",
                table: "HorasTrabalhadas");

            migrationBuilder.CreateIndex(
                name: "IX_HorasTrabalhadas_Funcionario_ID",
                table: "HorasTrabalhadas",
                column: "Funcionario_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_HorasTrabalhadas_Funcionario_Funcionario_ID",
                table: "HorasTrabalhadas",
                column: "Funcionario_ID",
                principalTable: "Funcionario",
                principalColumn: "Funcionario_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HorasTrabalhadas_Funcionario_Funcionario_ID",
                table: "HorasTrabalhadas");

            migrationBuilder.DropIndex(
                name: "IX_HorasTrabalhadas_Funcionario_ID",
                table: "HorasTrabalhadas");

            migrationBuilder.AddColumn<int>(
                name: "Funcionario_ID1",
                table: "HorasTrabalhadas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HorasTrabalhadas_Funcionario_ID1",
                table: "HorasTrabalhadas",
                column: "Funcionario_ID1");

            migrationBuilder.AddForeignKey(
                name: "FK_HorasTrabalhadas_Funcionario_Funcionario_ID1",
                table: "HorasTrabalhadas",
                column: "Funcionario_ID1",
                principalTable: "Funcionario",
                principalColumn: "Funcionario_ID");
        }
    }
}
