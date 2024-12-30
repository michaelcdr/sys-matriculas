using System.Collections.Generic;

namespace SysMatriculas.Dominio;

public class Curriculo
{
    public int CurriculoId { get; set; }
    public string Nome { get; set; }
    public int CursoId { get; set; }
    public Curso Curso { get; set; }
    public ICollection<Disciplina> Disciplinas { get; set; }
    public ICollection<Matricula> Matriculas { get; set; }

    public Curriculo()
    {
    }

    public Curriculo(string nome)
    {
        this.Nome = nome;
    }
}