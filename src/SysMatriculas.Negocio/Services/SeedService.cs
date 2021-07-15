using Microsoft.AspNetCore.Identity;
using SysMatriculas.Dominio;
using SysMatriculas.Persistencia.Seed;

namespace SysMatriculas.Negocio.Services
{
    public class SeedService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<TipoDeUsuario> _roleManager;

        public SeedService(UserManager<Usuario> userManager, RoleManager<TipoDeUsuario> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Seed()
        {
            var seed = new IdentitySeed(_userManager, _roleManager);
            seed.SeedData();
        }
    }
}
