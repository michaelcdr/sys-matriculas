using Microsoft.AspNetCore.Identity;
using SysMatriculas.Dominio;
using SysMatriculas.Persistencia.EF.Data;
using SysMatriculas.Persistencia.Seed;
using System.Linq;

namespace SysMatriculas.Negocio.Services
{
    public interface ISeedService
    {
        void Seed();
    }

    public class SeedService: ISeedService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<TipoDeUsuario> _roleManager;
        private readonly AppDbContext _db;

        public SeedService(UserManager<Usuario> userManager, 
                           RoleManager<TipoDeUsuario> roleManager, 
                           AppDbContext appDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = appDbContext;
        }

        public void Seed()
        {
            //var seed = new IdentitySeed(_userManager, _roleManager);
            //seed.SeedData();


            // curso > curriculos
            if (_db.Cursos.Count() == 0)
            {
                var curso = new Curso("Analise e desenvolvimento de sistemas", "Noite")
                {
                    Curriculos = new System.Collections.Generic.List<Curriculo>
                {
                    new Curriculo("146H")
                    {

                    }
                }
                };

                _db.Cursos.Add(curso);
                _db.SaveChanges();
            }
        }
    }
}
