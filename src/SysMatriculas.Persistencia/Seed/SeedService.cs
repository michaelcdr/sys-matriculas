using Microsoft.AspNetCore.Identity;
using SysMatriculas.Dominio;
using SysMatriculas.Persistencia.EF.Data;
using System;
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
            var curso1 = new Curso("Analise e desenvolvimento de sistemas", "Noite")
            {
                Curriculos = new List<Curriculo>
                {
                    new Curriculo("146F"),
                    new Curriculo("146G"),
                    new Curriculo("146H")
                }
            };

            var curso2 = new Curso("Sistemas de Informação", "Noite")
            {
                Curriculos = new List<Curriculo>
                {
                    new Curriculo("141H", new List<Disciplina>
                    {
                        new Disciplina(0, "Universidade e Sociedade", 60, 1),
                        new Disciplina(0, "Fundamentos de Administração", 60, 1),
                        new Disciplina(0, "Fundamentos Teóricos da Computação", 60, 1),
                        new Disciplina(0, "Introdução aos Sistemas de Informação", 60, 1),
                        new Disciplina(0, "Lógica de Programação", 60, 1),

                        new Disciplina(0, "Modelagem de Processos", 60, 1),
                        new Disciplina(0, "Matemática Discreta", 60, 1),
                        new Disciplina(0, "Programação Procedural", 60, 1),
                    })
                }
            };

            _db.Cursos.AddRange(curso1, curso2);
            _db.SaveChanges();

        }
    }

    private void CriarPrimeiroCoordenador()
    {
        if (!_userManager.GetUsersInRoleAsync("Coordenador").Result.Any())
        {
            var usuario = new Usuario()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "coordemador@sysmatriculas.com",
                Nome = "Coordenador",
                UserName = "coordenador",
                SobreNome = "Sem Sobrenome"
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
            TipoDeUsuario role = new TipoDeUsuario 
            { 
                Id = Guid.NewGuid().ToString(),
                Name = "Coordenador" 
            };
            IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
        }
    }

    private void CriaTipoDeUsuarioAlunoSeNaoExiste()
    {
        if (!_roleManager.RoleExistsAsync("Aluno").Result)
        {
            TipoDeUsuario role = new TipoDeUsuario 
            { 
                Id = Guid.NewGuid().ToString(), 
                Name = "Aluno" 
            };
            IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
        }
    }
}
