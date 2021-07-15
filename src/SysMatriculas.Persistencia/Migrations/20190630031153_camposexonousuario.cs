using Microsoft.EntityFrameworkCore.Migrations;

namespace SysMatriculas.Persistencia.Migrations
{
    public partial class camposexonousuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sexo",
                table: "Usuarios",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "Usuarios");
        }
    }
}
