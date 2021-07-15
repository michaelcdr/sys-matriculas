using SysMatriculas.Dominio;
using SysMatriculas.Web.ViewModels;

namespace SysMatriculas.Web.Extensions
{
    public static class DisciplinaEdicaoViewModelExtensions
    {
        public static DisciplinaRequest ToDisciplinaRequest(this DisciplinaEdicaoViewModel disciplina)
        {
            var disciplinaNova = new DisciplinaRequest(
                disciplina.Nome,
                (int)disciplina.CurriculoId,
                (int)disciplina.CargaHoraria,
                (int)disciplina.Semestre,
                disciplina.PreRequisitos,
                disciplina.CoRequisitos,
                disciplina.Ordem
            )
            {
                DisciplinaId = disciplina.DisciplinaId
            };
            return disciplinaNova;
        }
    }
}
