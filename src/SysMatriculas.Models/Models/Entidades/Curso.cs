using System.Collections.Generic;

namespace SysMatriculas.Dominio;

public class Curso
{
    public int CursoId { get; set; }
    public string Nome { get; set; }
    public string Turno { get; set; }
    public bool Ativo { get; set; }
    public List<Curriculo> Curriculos { get; set; }

    public Curso()
    {
    }

    public Curso(string nome, string turno)
    {
        this.Nome = nome;
        this.Turno = turno;
    }

    public void Desativar()
    {
        Ativo = false;
    }

    public void Ativar()
    {
        Ativo = true;
    }
}
