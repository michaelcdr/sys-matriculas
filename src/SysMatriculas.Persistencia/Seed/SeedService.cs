using Microsoft.AspNetCore.Identity;
using SysMatriculas.Dominio;
using SysMatriculas.Persistencia.EF.Data;
using System.Collections.Generic;
using System.Linq;

namespace SysMatriculas.Persistencia.Seed;

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
        CriaTipoDeUsuarioAlunoSeNaoExiste();
        CriarTipoDeUsuarioCoordenadorSeNaoExiste();
        CriarPrimeiroCoordenador();

        // curso > curriculos
        if (_db.Cursos.Count() == 0)
        {
            var curso = new Curso("Analise e desenvolvimento de sistemas", "Noite")
            {
                Curriculos = new List<Curriculo>
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
