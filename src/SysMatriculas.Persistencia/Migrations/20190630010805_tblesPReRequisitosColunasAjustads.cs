using Microsoft.EntityFrameworkCore.Migrations;

namespace SysMatriculas.Persistencia.Migrations
{
    public partial class tblesPReRequisitosColunasAjustads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoRequisitos_Disciplinas_DisciplinaCoRequisitoId",
                table: "CoRequisitos");

            migrationBuilder.RenameColumn(
                name: "DisciplinaCoRequisitoId",
                table: "CoRequisitos",
                newName: "DisciplinaPaiId");

            migrationBuilder.RenameIndex(
                name: "IX_CoRequisitos_DisciplinaCoRequisitoId",
                table: "CoRequisitos",
                newName: "IX_CoRequisitos_DisciplinaPaiId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoRequisitos_Disciplinas_DisciplinaPaiId",
                table: "CoRequisitos",
                column: "DisciplinaPaiId",
                principalTable: "Disciplinas",
                principalColumn: "DisciplinaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoRequisitos_Disciplinas_DisciplinaPaiId",
                table: "CoRequisitos");

            migrationBuilder.RenameColumn(
                name: "DisciplinaPaiId",
                table: "CoRequisitos",
                newName: "DisciplinaCoRequisitoId");

            migrationBuilder.RenameIndex(
                name: "IX_CoRequisitos_DisciplinaPaiId",
                table: "CoRequisitos",
                newName: "IX_CoRequisitos_DisciplinaCoRequisitoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoRequisitos_Disciplinas_DisciplinaCoRequisitoId",
                table: "CoRequisitos",
                column: "DisciplinaCoRequisitoId",
                principalTable: "Disciplinas",
                principalColumn: "DisciplinaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
