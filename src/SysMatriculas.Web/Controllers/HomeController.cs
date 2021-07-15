using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SysMatriculas.Dominio;
using SysMatriculas.Persistencia.EF.Data;
using SysMatriculas.Web.Models;

namespace SysMatriculas.Web.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _db;

        public HomeController(AppDbContext context)
        {
            _db = context;
        }

        public IActionResult Seed()
        {

            //var cursos = _db.Cursos.ToList();
            //var disciplinas = _db.Disciplinas.Include(a => a.PreRequisitos)
            //                                 .Include(a => a.PreRequisitosBase)
            //                                 .ToList();
            //var ds1 = new Disciplina
            //{
            //    Nome = "Programação para Dispositivos Móveis",
            //    CargaHoraria = 80,
            //    Semestre = 5

            //};
            //_db.Disciplinas.Add(ds1);
            //_db.SaveChanges();

            //var ds2 = new Disciplina
            //{

            //    Nome = "Programação Orientada a Objetos",
            //    CargaHoraria = 80,
            //    Semestre = 2
            //};
            //_db.Disciplinas.Add(ds2);
            //_db.SaveChanges();

            //_db.PreRequisitos.Add(new PreRequisito
            //{
            //    DisciplinaId = ds1.DisciplinaId,
            //    DisciplinaPreRequisitoId = ds2.DisciplinaId
            //});
            //_db.SaveChanges();

            return View();
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
