using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Responses;
using SysMatriculas.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SysMatriculas.Web.Extensions
{
    public static class DisciplinaExtensions
    {
        public static DisciplinaResponse ToDisciplinaResponse(this Disciplina disciplina)
        {
            return new DisciplinaResponse(disciplina.DisciplinaId, disciplina.Nome);
        }

        public static DisciplinaEdicaoViewModel ToDisciplinaEdicaoViewModel(this Disciplina disciplina)
        {
            return new DisciplinaEdicaoViewModel
            {
                DisciplinaId = disciplina.DisciplinaId,
                CargaHoraria = disciplina.CargaHoraria,
                CurriculoId = disciplina.CurriculoId,
                Nome = disciplina.Nome,
                Semestre = disciplina.Semestre,
                CoRequisitos = disciplina.CoRequisitos != null
                    ? disciplina.CoRequisitos.Select(c => c.DisciplinaId).ToList()
                    : new List<int>(),
                PreRequisitos = disciplina.PreRequisitos != null
                    ? disciplina.PreRequisitos.Select(e => e.DisciplinaId).ToList()
                    : new List<int>()
            };
        }
    }
}
