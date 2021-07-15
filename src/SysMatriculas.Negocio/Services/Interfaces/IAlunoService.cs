using SysMatriculas.Dominio;
using System.Threading.Tasks;

namespace SysMatriculas.Negocio.Services.Interfaces
{
    public interface IAlunoService
    {
        Task Matricular(MatricularRequest request);
        Task ConcluirDisciplina(ConcluirDisciplinaRequest request);
    }
}
