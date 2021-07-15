using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SysMatriculas.Dominio;
using SysMatriculas.Web.ViewModels;
using System.Threading.Tasks;

namespace SysMatriculas.Web.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly UserManager<Usuario> _userManager;

        public MenuViewComponent(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (currentUser == null)
                return View();

            bool coordenador = User.IsInRole("Coordenador");
            var model = new MenuComponent
            {
                Nome = currentUser.Nome,
                Email = currentUser.Email,
                Login = currentUser.UserName,
                TipoDeUsuario = User.IsInRole("Coordenador") ? "Coordenador": "Aluno"
            };
            return View(model);
        }
    }
}
