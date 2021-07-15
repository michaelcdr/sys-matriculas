using Microsoft.AspNetCore.Identity;
using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Requests;
using SysMatriculas.Dominio.Responses;
using SysMatriculas.Negocio.Exceptions;
using SysMatriculas.Negocio.Services.Interfaces;
using SysMatriculas.Persistencia.Transacoes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysMatriculas.Negocio.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<TipoDeUsuario> _roleManager;
        private readonly IUnitOfWork _uow;

        public UsuarioService(
            UserManager<Usuario> userManager, 
            SignInManager<Usuario> signInManager,
            RoleManager<TipoDeUsuario> roleManager,
            IUnitOfWork uow)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _uow = uow;
        }

        public async Task Ativar(object id)
        {
            await _uow.Usuarios.Ativar(id);
            await _uow.CommitAsync();
        }

        public async Task Desativar(object id)
        {
            await _uow.Usuarios.Desativar(id);
            await _uow.CommitAsync();
        }

        public async Task Cadastrar(UsuarioRequest model)
        { 
            var usuario = new Usuario
            {
                UserName = model.Login,
                Email = model.Email,
                Nome = model.Nome,
                SobreNome = model.SobreNome,
                PasswordHash = model.Senha,
                CPF = model.CPF,
                Sexo = model.Sexo,
                Ativo = true
            };

            var erros = new List<string>();
            IdentityResult resultado = await _userManager.CreateAsync(usuario, model.Senha);
            if (!resultado.Succeeded)
            {
                erros.AddRange(GenerateModelErrors(resultado.Errors));
                throw new UsuarioCadastroException(string.Join("-", erros));
            }

            //var roleAluno = await _roleManager.FindByNameAsync("Aluno");
            //if (roleAluno == null)
            //    await _roleManager.CreateAsync(new TipoDeUsuario { Name = "Aluno" });

            //var roleCoordenador = await _roleManager.FindByNameAsync("Coordenador");
            //if (roleCoordenador == null)
            //    await _roleManager.CreateAsync(new TipoDeUsuario { Name = "Coordenador" });

            IdentityResult resultadoRole = await _userManager.AddToRoleAsync(usuario, model.TipoDeUsuario);
            if (!resultadoRole.Succeeded)
            {
                erros.AddRange(GenerateModelErrors(resultado.Errors));
                throw new TipoDeUsuarioCadastroException(string.Join("-", erros));
            }
        }

        public async Task<LoginResponse> Logar(LoginRequest request)
        {
            Usuario usuario = await _uow.Usuarios.ObterPeloLogin(request.UserName);

            var resultadoLogin = await _signInManager.PasswordSignInAsync(
                request.UserName,
                request.Senha,
                true,
                lockoutOnFailure: false
            );

            string tipoDeUsuario = await _userManager.IsInRoleAsync(usuario, "Aluno") ? "Aluno" : "Coordenador";

            if (resultadoLogin.Succeeded)
                return new LoginResponse(true,tipoDeUsuario,"Login efetuado com sucesso.");

            return new LoginResponse(false, "", "Não foi possível efetuar o login.");
        }

        public async Task<ColecaoPaginada<Usuario>> ObterListaPaginada(DataTableRequest request, string tipo)
        {
            ColecaoPaginada<Usuario> usuariosPaginados = await _uow.Usuarios.ObterListaPaginada(request, tipo);
            return usuariosPaginados;
        }

        private List<string> GenerateModelErrors(IEnumerable<IdentityError> errors)
        {
            var erros = new List<string>();
            foreach (var error in errors)
                erros.Add($"{error.Code} - ${error.Description}");
            return erros;
        }

        public async Task Remover(string id)
        {
            Usuario usuario = await _userManager.FindByIdAsync(id);
            IList<string> tiposDeUsuarioDoUsuario = await _userManager.GetRolesAsync(usuario);

            foreach (var item in tiposDeUsuarioDoUsuario)
            {
                var result = await _userManager.RemoveFromRoleAsync(usuario, item);
            }

            await _userManager.DeleteAsync(usuario);
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<string> ObterTipoDeUsuario(Usuario user)
        {
            return (await _userManager.GetRolesAsync(user)).FirstOrDefault();
        }

        public async Task<Usuario> Obter(object id)
        {
            return (await _uow.Usuarios.Obter(id));
        }

        public async Task Atualizar(UsuarioRequest request)
        {
            Usuario usuario = await _uow.Usuarios.Obter(request.UsuarioId);
            usuario.CPF = request.CPF;
            usuario.Email = request.Email;
            usuario.Nome = request.Nome;
            usuario.SobreNome = request.SobreNome;
            usuario.Sexo = request.Sexo;
            await _userManager.UpdateAsync(usuario);
        }

        public async Task<List<Usuario>> ObterTodosAlunos()
        {
            return await _uow.Usuarios.ObterTodosAlunos();
        }

        public async Task Matricular(int curriculoId, string usuarioId)
        {
            _uow.Matriculas.Cadastrar(new Matricula { CurriculoId = curriculoId, UsuarioId = usuarioId });
            await _uow.CommitAsync();
        }

        public async Task<Usuario> ObterUsuarioAtual(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
    }
}
