using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Responses;
using SysMatriculas.Persistencia.DTOs.DataTables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysMatriculas.Negocio.Services.Interfaces;

public interface IUsuarioService
{
    Task<LoginResponse> Logar(LoginRequest request);
    Task Cadastrar(UsuarioRequest request);
    Task<ColecaoPaginada<Usuario>> ObterListaPaginada(DataTableRequest request, string tipo);
    Task Ativar(object id);
    Task Desativar(object id);
    Task Remover(string id);
    Task LogOut();

    Task<string> ObterTipoDeUsuario(Usuario user);
    Task<Usuario> Obter(object id);
    Task Atualizar(UsuarioRequest request);
    Task<List<Usuario>> ObterTodosAlunos();
    Task Matricular(int curriculoId, string usuarioId);
    Task<Usuario> ObterUsuarioAtual(string userName);
}
