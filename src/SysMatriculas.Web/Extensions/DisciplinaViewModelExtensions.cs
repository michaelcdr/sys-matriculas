using SysMatriculas.Dominio;
using SysMatriculas.Web.ViewModels;

namespace SysMatriculas.Web.Extensions
{
    public static class DisciplinaViewModelExtensions
    {
        public static DisciplinaRequest ToDisciplinaRequest(this DisciplinaCadastroViewModel disciplina)
        {
            var disciplinaNova = new DisciplinaRequest(
                disciplina.Nome,
                (int)disciplina.CurriculoId,
                (int)disciplina.CargaHoraria,
                (int)disciplina.Semestre,
                disciplina.PreRequisitos,
                disciplina.CoRequisitos,
                disciplina.Ordem
            );
            return disciplinaNova;
        }
    }
}
