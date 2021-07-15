using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Requests;
using SysMatriculas.Dominio.Responses;
using SysMatriculas.Persistencia.EF.Data;
using SysMatriculas.Persistencia.Repositorios.Interfaces;

namespace SysMatriculas.Persistencia.Repositorios
{
    public class PreRequisitosRepositorio : Repositorio<PreRequisito>, IPreRequisitosRepositorio
    {
        private AppDbContext _context;

        public PreRequisitosRepositorio(AppDbContext context) : base(context)
        {
            _context = context;
        }

        
    }
}
