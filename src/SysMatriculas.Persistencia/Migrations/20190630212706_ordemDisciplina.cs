using Microsoft.EntityFrameworkCore.Migrations;

namespace SysMatriculas.Persistencia.Migrations
{
    public partial class ordemDisciplina : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ordem",
                table: "Disciplinas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ordem",
                table: "Disciplinas");
        }
    }
}
