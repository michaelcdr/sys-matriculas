using System.Collections.Generic;

namespace SysMatriculas.Dominio;

public class Curriculo
{
    public int CurriculoId { get; set; }
    public string Nome { get; private set; }
    public int CursoId { get; private set; }
    public Curso Curso { get; set; }
    public ICollection<Disciplina> Disciplinas { get; set; }
    public ICollection<Matricula> Matriculas { get; set; }

    protected Curriculo()
    {
        Disciplinas = new HashSet<Disciplina>();
        Matriculas = new HashSet<Matricula>();
    }

    public Curriculo(string nome, List<Disciplina> disciplinas = null, List<Matricula> matriculas = null)
    {
        Nome = nome;
        Disciplinas = disciplinas ?? new List<Disciplina>();
        Matriculas = matriculas ?? new List<Matricula>();
    }

    public void Atualizar(string nome)
    {
        Nome = nome;
    }
}