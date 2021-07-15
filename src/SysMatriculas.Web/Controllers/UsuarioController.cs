using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Requests;
using SysMatriculas.Dominio.Responses;
using SysMatriculas.Negocio.Exceptions;
using SysMatriculas.Negocio.Services.Interfaces;
using SysMatriculas.Web.Extensions;
using SysMatriculas.Web.Models;
using SysMatriculas.Web.ViewModels;
using System;
using System.Threading.Tasks;

namespace SysMatriculas.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginSistema model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            try
            {
                if (ModelState.IsValid)
                {
                    LoginResponse response = await _usuarioService.Logar(new LoginRequest(model.UserName, model.Password));
                    if (response.Logado)
                    {
                        if (response.TipoDeUsuario == "Aluno")
                            return RedirectToAction("Curriculos", "Aluno");
                        else
                            return RedirectToAction("Index", "Curso");
                    }
                }
                ModelState.AddModelError(string.Empty, $"Dados inválidos.");
            }
            catch (LoginNaoRealizadoException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Dados inválidos.");
            }
            return View(model);
        }

        [Authorize(Roles = "Coordenador")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Listar(DataTableRequest request)
        {
            try
            {
                ColecaoPaginada<Usuario> cursosPaginados = 
                    await _usuarioService.ObterListaPaginada(request, "Coordenador");

                return Json(new
                {
                    request.draw,
                    recordsTotal = cursosPaginados.RecordsTotal,
                    recordsFiltered = cursosPaginados.RecordsFiltered,
                    data = cursosPaginados.Itens
                });
            }
            catch (Exception ex)
            {
                return Json(new ErroResponse("Não foi possível obter os cursos", ex.Message));
            }
        }

        [Authorize(Roles = "Coordenador")]
        public async Task<IActionResult> Cadastrar()
        {
            return View(new UsuarioCadastro());
        }

        [HttpPost]
        [Authorize(Roles = "Coordenador")]
        public async Task<IActionResult> Cadastrar(UsuarioCadastro model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioRequest request = model.ToUsuarioRequest();
                    request.TipoDeUsuario = "Coordenador";
                    await _usuarioService.Cadastrar(request);
                    return RedirectToAction("index");
                }
            }
            catch (UsuarioCadastroException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (TipoDeUsuarioCadastroException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ErrorModel.Msg);
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Coordenador")]
        public async Task<JsonResult> Remover(string id)
        {
            try
            {
                await _usuarioService.Remover(id);
                return Json(new SucessoResponse("Usuário excluído com sucesso."));
            }
            catch (Exception ex)
            {
                return Json(new ErroResponse("Não foi possível excluir.", ex.Message));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _usuarioService.LogOut();
            return RedirectToAction("Login", "Usuario");
        }

        [Authorize(Roles = "Coordenador")]
        public async Task<IActionResult> Editar(string id)
        {
            Usuario usuario = await _usuarioService.Obter(id);
            CoordenadorEdit viewModel = usuario.ToCoordenadorEdit();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Coordenador")]
        public async Task<IActionResult> Editar(AlunoEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioRequest request = model.ToUsuarioRequest();
                    request.UsuarioId = model.UsuarioId;
                    await _usuarioService.Atualizar(request);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ErrorModel.Msg);
            }
            return View(model);
        }
    }
}