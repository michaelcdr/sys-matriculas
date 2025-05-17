using System;
using System.Collections.Generic;

namespace SysMatriculas.Dominio;

public class Curso
{
    public int CursoId { get; set; }
    public string Nome { get; private set; }
    public string Turno { get; private set; }
    public bool Ativo { get; private set; }
    public List<Curriculo> Curriculos { get; set; }

    protected Curso()
    {
    }

    public Curso(string nome, string turno)
    {
        Nome = nome;
        Turno = turno;
        Ativo = true;
    }

    public void Desativar()
    {
        Ativo = false;
    }

    public void Ativar()
    {
        Ativo = true;
    }

    public void Atualizar(string nome, string turno)
    {
        Nome = nome;
        Turno = turno;
    }
}
