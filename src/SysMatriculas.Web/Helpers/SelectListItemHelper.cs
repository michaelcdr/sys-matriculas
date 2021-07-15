using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SysMatriculas.Dominio;
using SysMatriculas.Negocio.Services.Interfaces;

namespace SysMatriculas.Web.Helpers
{
    public class SelectListItemHelper : ISelectListItemHelper
    {
        private readonly IAlunoService _alunoService;
        private readonly IDisciplinaService _disciplinaService;
        private readonly ICurriculoService _curriculoService;
        private readonly IUsuarioService _usuarioService;
        private readonly ICursoService _cursoService;

        public SelectListItemHelper(
            IAlunoService alunoService, 
            IDisciplinaService disciplinaService,
            ICurriculoService curriculoService,
            IUsuarioService usuarioService,
            ICursoService cursoService)
        {
            _alunoService = alunoService;
            _disciplinaService = disciplinaService;
            _curriculoService = curriculoService;
            _usuarioService = usuarioService;
            _cursoService = cursoService;
        }

        public async Task<List<SelectListItem>> ObterAlunos()
        {
            List<Usuario> alunos = await _usuarioService.ObterTodosAlunos();
            return alunos.Select(e => new SelectListItem(e.Nome, e.Id)).ToList();
        }

        public async Task<List<SelectListItem>> ObterCursos()
        {
            List<Curso> cursos = await _cursoService.ObterTodos();
            List<SelectListItem> cursosSelectListItem = cursos.Select(c => new SelectListItem(c.Nome, c.CursoId.ToString()))
                                                              .ToList();
            return cursosSelectListItem;
        }

        public async Task<List<SelectListItem>> ObterSelectListDisciplinas(int curriculoId, int? disciplinaAtual)
        {
            List<Disciplina> disciplinas = await _disciplinaService.ObterTodasDoCurriculo(curriculoId);
            disciplinas = disciplinas.Where(e => e.DisciplinaId != disciplinaAtual).ToList();
            List<SelectListItem> disciplinasSelectList = disciplinas.Select(d => new SelectListItem(d.Nome, d.DisciplinaId.ToString()))
                                                          .ToList();
            return disciplinasSelectList;
        }

        public async Task<List<SelectListItem>> ObterDisciplinasSelectList()
        {
            List<Disciplina> disciplinas = await _disciplinaService.ObterTodas();
            var disciplinasItens = disciplinas.Select(e => new SelectListItem(e.Nome, e.DisciplinaId.ToString()))
                                              .ToList();
            return disciplinasItens;
        }

        public async Task<List<SelectListItem>> ObterCurriculosSelectList()
        {
            List<Curriculo> curriculos = await _curriculoService.ObterTodos();
            var selectListItems = curriculos.Select(
                item => new SelectListItem(
                    $"{item.Curso.Nome} - {item.Nome}", item.CurriculoId.ToString())
            ).ToList();
            return selectListItems;
        }
    }
}
