using Microsoft.AspNetCore.Identity;
using SysMatriculas.Dominio;
using System.Linq;

namespace SysMatriculas.Persistencia.Seed
{
    public class IdentitySeed
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<TipoDeUsuario> _roleManager;

        public IdentitySeed(UserManager<Usuario> userManager, 
                            RoleManager<TipoDeUsuario> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedData()
        {
            CriaTipoDeUsuarioAlunoSeNaoExiste();
            CriarTipoDeUsuarioCoordenadorSeNaoExiste();
            CriarPrimeiroCoordenador();
        }

        private void CriarPrimeiroCoordenador()
        {
            if (!_userManager.GetUsersInRoleAsync("Coordenador").Result.Any())
            {
                var usuario = new Usuario()
                {
                    Email = "michaelcdr@hotmail.com",
                    Nome = "Michael",
                    UserName = "michael",
                    SobreNome = "Costa dos Reis"
                };
                var result = _userManager.CreateAsync(usuario, "123456").Result;

                if (result.Succeeded)
                {
                    var resultCreatedRole = _userManager.AddToRoleAsync(usuario, "Coordenador").Result;
                }
            }
        }

        private void CriarTipoDeUsuarioCoordenadorSeNaoExiste()
        {
            if (!_roleManager.RoleExistsAsync("Coordenador").Result)
            {
                TipoDeUsuario role = new TipoDeUsuario { Name = "Coordenador" };
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }

        private void CriaTipoDeUsuarioAlunoSeNaoExiste()
        {
            if (!_roleManager.RoleExistsAsync("Aluno").Result)
            {
                TipoDeUsuario role = new TipoDeUsuario { Name = "Aluno" };
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }
    }
}
