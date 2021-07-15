using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SysMatriculas.Persistencia.Migrations
{
    public partial class tbmatriculas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurriculosDosUsuarios");

            migrationBuilder.AddColumn<int>(
                name: "DisciplinaId",
                table: "UsuariosDesempenhos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Matriculas",
                columns: table => new
                {
                    MatriculaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<string>(nullable: true),
                    CurriculoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matriculas", x => x.MatriculaId);
                    table.ForeignKey(
                        name: "FK_Matriculas_Curriculos_CurriculoId",
                        column: x => x.CurriculoId,
                        principalTable: "Curriculos",
                        principalColumn: "CurriculoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matriculas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosDesempenhos_DisciplinaId",
                table: "UsuariosDesempenhos",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_CurriculoId",
                table: "Matriculas",
                column: "CurriculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_UsuarioId",
                table: "Matriculas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosDesempenhos_Disciplinas_DisciplinaId",
                table: "UsuariosDesempenhos",
                column: "DisciplinaId",
                principalTable: "Disciplinas",
                principalColumn: "DisciplinaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosDesempenhos_Disciplinas_DisciplinaId",
                table: "UsuariosDesempenhos");

            migrationBuilder.DropTable(
                name: "Matriculas");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosDesempenhos_DisciplinaId",
                table: "UsuariosDesempenhos");

            migrationBuilder.DropColumn(
                name: "DisciplinaId",
                table: "UsuariosDesempenhos");

            migrationBuilder.CreateTable(
                name: "CurriculosDosUsuarios",
                columns: table => new
                {
                    CurriculoUsuarioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurriculoId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculosDosUsuarios", x => x.CurriculoUsuarioId);
                    table.ForeignKey(
                        name: "FK_CurriculosDosUsuarios_Curriculos_CurriculoId",
                        column: x => x.CurriculoId,
                        principalTable: "Curriculos",
                        principalColumn: "CurriculoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurriculosDosUsuarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurriculosDosUsuarios_CurriculoId",
                table: "CurriculosDosUsuarios",
                column: "CurriculoId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculosDosUsuarios_UsuarioId",
                table: "CurriculosDosUsuarios",
                column: "UsuarioId");
        }
    }
}
