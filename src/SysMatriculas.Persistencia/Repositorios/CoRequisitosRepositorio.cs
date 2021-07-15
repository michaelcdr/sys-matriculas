using SysMatriculas.Dominio;
using SysMatriculas.Persistencia.EF.Data;
using SysMatriculas.Persistencia.Repositorios.Interfaces;

namespace SysMatriculas.Persistencia.Repositorios
{
    public class CoRequisitosRepositorio : Repositorio<CoRequisito>, ICoRequisitosRepositorio
    {
        private AppDbContext _context;

        public CoRequisitosRepositorio(AppDbContext context) : base(context)
        {
            _context = context;
        }

        
    }
}
