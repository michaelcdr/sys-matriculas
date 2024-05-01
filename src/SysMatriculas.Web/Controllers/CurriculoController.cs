using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Requests;
using SysMatriculas.Dominio.Responses;
using SysMatriculas.Negocio.Exceptions;
using SysMatriculas.Negocio.Services.Interfaces;
using SysMatriculas.Web.Extensions;
using SysMatriculas.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysMatriculas.Web.Controllers
{
    [Authorize(Roles = "Coordenador")]
    public class CurriculoController : Controller
    {
        private readonly ICurriculoService _curriculoService;
        private readonly ICursoService _cursoService;
        private readonly ISelectListItemHelper _selectListHelper;

        public CurriculoController(ICurriculoService serviceCurriculo,
                                   ICursoService cursoService,
                                   ISelectListItemHelper selectListItemHelper)
        {
            _curriculoService = serviceCurriculo;
            _cursoService = cursoService;
            _selectListHelper = selectListItemHelper;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> ObterLista()
        {
            try
            {
                List<Curriculo> curriculos = await _curriculoService.ObterTodos();

                var curriculosRequest = curriculos.Select(item => item.ToCurriculoRequest())
                                                  .ToList();

                return Json(new { sucesso = true, curriculos = curriculosRequest });
            }
            catch (Exception ex)
            {
                return Json(
                    new ErroResponse(
                        "Não foi possível obter uma lista de currículos.", 
                        ex.Message
                    )
                );
            }
        }

        [HttpPost]
        public async Task<JsonResult> Listar(DataTableRequest request)
        {
            try
            {
                var curriculosPaginados = await _curriculoService.ObterListaPaginada(request);

                return Json(new
                {
                    request.draw,
                    recordsTotal = curriculosPaginados.RecordsTotal,
                    recordsFiltered = curriculosPaginados.RecordsFiltered,
                    data = curriculosPaginados.Itens
                });
            }
            catch (Exception ex)
            {
                return Json(new ErroResponse("Não foi possível obter os estabelecimentos", ex.Message));
            }
        }

        public async Task<IActionResult> Cadastrar()
        {
            ViewBag.Cursos = await _selectListHelper.ObterCursos();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Curriculo curriculo, IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _curriculoService.Cadastrar(curriculo);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (CurriculoJaCadastradoException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            ViewBag.Cursos = await _selectListHelper.ObterCursos();
            return View(curriculo);            
        }

        public async Task<ActionResult> Editar(int id)
        {
            Curriculo curriculo = await _curriculoService.Obter(id);
            ViewBag.Cursos = await _selectListHelper.ObterCursos();
            return View(curriculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Curriculo model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var request = new CurriculoRequest(model.CurriculoId, model.Nome);
                    await _curriculoService.Atualizar(request);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Não foi possivel atualizar o currículo.");
            }
            ViewBag.Cursos = await _selectListHelper.ObterCursos();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Deletar(int id, IFormCollection collection)
        {
            try
            {
                await _curriculoService.Delete(id);
                return Json(new { sucesso = true });
            }
            catch (CurriculoEmUsoException ex)
            {
                return Json(new ErroResponse("Não foi possível remover o currículo pois ele esta em uso.", ex.Message));
            }
            catch (Exception ex)
            {
                return Json(new ErroResponse("Não foi possível remover o currículo.", ex.Message));
            }
        }
    }
}