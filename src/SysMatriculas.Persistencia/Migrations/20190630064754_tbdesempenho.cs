using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SysMatriculas.Persistencia.Migrations
{
    public partial class tbdesempenho : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurriculoUsuario_Curriculos_CurriculoId",
                table: "CurriculoUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_CurriculoUsuario_Usuarios_UsuarioId",
                table: "CurriculoUsuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurriculoUsuario",
                table: "CurriculoUsuario");

            migrationBuilder.RenameTable(
                name: "CurriculoUsuario",
                newName: "CurriculosDosUsuarios");

            migrationBuilder.RenameIndex(
                name: "IX_CurriculoUsuario_UsuarioId",
                table: "CurriculosDosUsuarios",
                newName: "IX_CurriculosDosUsuarios_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_CurriculoUsuario_CurriculoId",
                table: "CurriculosDosUsuarios",
                newName: "IX_CurriculosDosUsuarios_CurriculoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurriculosDosUsuarios",
                table: "CurriculosDosUsuarios",
                column: "CurriculoUsuarioId");

            migrationBuilder.CreateTable(
                name: "UsuariosDesempenhos",
                columns: table => new
                {
                    UsuarioDesempenhoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<string>(nullable: true),
                    Aprovado = table.Column<bool>(nullable: false),
                    Nota = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosDesempenhos", x => x.UsuarioDesempenhoId);
                    table.ForeignKey(
                        name: "FK_UsuariosDesempenhos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosDesempenhos_UsuarioId",
                table: "UsuariosDesempenhos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurriculosDosUsuarios_Curriculos_CurriculoId",
                table: "CurriculosDosUsuarios",
                column: "CurriculoId",
                principalTable: "Curriculos",
                principalColumn: "CurriculoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurriculosDosUsuarios_Usuarios_UsuarioId",
                table: "CurriculosDosUsuarios",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurriculosDosUsuarios_Curriculos_CurriculoId",
                table: "CurriculosDosUsuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_CurriculosDosUsuarios_Usuarios_UsuarioId",
                table: "CurriculosDosUsuarios");

            migrationBuilder.DropTable(
                name: "UsuariosDesempenhos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurriculosDosUsuarios",
                table: "CurriculosDosUsuarios");

            migrationBuilder.RenameTable(
                name: "CurriculosDosUsuarios",
                newName: "CurriculoUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_CurriculosDosUsuarios_UsuarioId",
                table: "CurriculoUsuario",
                newName: "IX_CurriculoUsuario_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_CurriculosDosUsuarios_CurriculoId",
                table: "CurriculoUsuario",
                newName: "IX_CurriculoUsuario_CurriculoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurriculoUsuario",
                table: "CurriculoUsuario",
                column: "CurriculoUsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurriculoUsuario_Curriculos_CurriculoId",
                table: "CurriculoUsuario",
                column: "CurriculoId",
                principalTable: "Curriculos",
                principalColumn: "CurriculoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurriculoUsuario_Usuarios_UsuarioId",
                table: "CurriculoUsuario",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
