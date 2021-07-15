using Microsoft.EntityFrameworkCore.Migrations;

namespace SysMatriculas.Persistencia.Migrations
{
    public partial class tbprrequisitoalterada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreRequisitos_Disciplinas_DisciplinaPreRequisitoId",
                table: "PreRequisitos");

            migrationBuilder.RenameColumn(
                name: "DisciplinaPreRequisitoId",
                table: "PreRequisitos",
                newName: "DisciplinaPaiId");

            migrationBuilder.RenameIndex(
                name: "IX_PreRequisitos_DisciplinaPreRequisitoId",
                table: "PreRequisitos",
                newName: "IX_PreRequisitos_DisciplinaPaiId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreRequisitos_Disciplinas_DisciplinaPaiId",
                table: "PreRequisitos",
                column: "DisciplinaPaiId",
                principalTable: "Disciplinas",
                principalColumn: "DisciplinaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreRequisitos_Disciplinas_DisciplinaPaiId",
                table: "PreRequisitos");

            migrationBuilder.RenameColumn(
                name: "DisciplinaPaiId",
                table: "PreRequisitos",
                newName: "DisciplinaPreRequisitoId");

            migrationBuilder.RenameIndex(
                name: "IX_PreRequisitos_DisciplinaPaiId",
                table: "PreRequisitos",
                newName: "IX_PreRequisitos_DisciplinaPreRequisitoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreRequisitos_Disciplinas_DisciplinaPreRequisitoId",
                table: "PreRequisitos",
                column: "DisciplinaPreRequisitoId",
                principalTable: "Disciplinas",
                principalColumn: "DisciplinaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
