using Microsoft.AspNetCore.Mvc;
using SysMatriculas.Web.Models;
using System.Diagnostics;

namespace SysMatriculas.Web.Controllers
{
    public class HomeController : Controller
    { 
        public HomeController()
        { 
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