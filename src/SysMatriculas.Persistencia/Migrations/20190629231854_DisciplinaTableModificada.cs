using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SysMatriculas.Persistencia.Migrations
{
    public partial class DisciplinaTableModificada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurriculosDisciplinas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurriculosDisciplinas",
                columns: table => new
                {
                    CurriculoDisciplinaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurriculoId = table.Column<int>(nullable: false),
                    DisciplinaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculosDisciplinas", x => x.CurriculoDisciplinaId);
                    table.ForeignKey(
                        name: "FK_CurriculosDisciplinas_Curriculos_CurriculoId",
                        column: x => x.CurriculoId,
                        principalTable: "Curriculos",
                        principalColumn: "CurriculoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurriculosDisciplinas_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "DisciplinaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurriculosDisciplinas_CurriculoId",
                table: "CurriculosDisciplinas",
                column: "CurriculoId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculosDisciplinas_DisciplinaId",
                table: "CurriculosDisciplinas",
                column: "DisciplinaId");
        }
    }
}
