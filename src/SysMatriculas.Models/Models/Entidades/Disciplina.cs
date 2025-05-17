using System.Collections.Generic;

namespace SysMatriculas.Dominio;

public class Disciplina
{
    public int DisciplinaId { get; set; }

    public string Nome { get; private set; }
    public int CargaHoraria { get; private set; }
    public int Semestre { get; private set; }
    public int? Ordem { get; set; }

    public ICollection<PreRequisito> PreRequisitos { get; private set; }
    public ICollection<PreRequisito> DisciplinasOndeEPreRequisito { get; private set; }

    public ICollection<CoRequisito> CoRequisitos { get; private set; }
    public ICollection<CoRequisito> DisciplinasOndeECoRequisito { get; private set; }

    public int CurriculoId { get; private set; }
    public Curriculo Curriculo { get; set; }

    public ICollection<Desempenho> Desempenhos { get; set; }

    protected Disciplina()
    {
        CoRequisitos = new HashSet<CoRequisito>();
        DisciplinasOndeECoRequisito = new HashSet<CoRequisito>();

        PreRequisitos = new HashSet<PreRequisito>();
        DisciplinasOndeEPreRequisito = new HashSet<PreRequisito>();

        Desempenhos = new HashSet<Desempenho>();
    }

    public Disciplina(int curriculoId, string nome, int cargaHoraria, int semestre)
    {
        CoRequisitos = new HashSet<CoRequisito>();
        DisciplinasOndeECoRequisito = new HashSet<CoRequisito>();

        PreRequisitos = new HashSet<PreRequisito>();
        DisciplinasOndeEPreRequisito = new HashSet<PreRequisito>();

        Desempenhos = new HashSet<Desempenho>();

        Nome = nome;
        CurriculoId = curriculoId;
        CargaHoraria = cargaHoraria;
        Semestre = semestre;

    }

    public void Atualizar(int curriculoId, int cargaHoraria, int semestre)
    {
        CurriculoId = curriculoId;
        CargaHoraria = cargaHoraria;
        Semestre = semestre;
    }
}
