using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SysMatriculas.Dominio;
using SysMatriculas.Web.ViewModels;
using System.Threading.Tasks;

namespace SysMatriculas.Web.ViewComponents
{
    public class UsuarioPerfilViewComponent : ViewComponent
    {
        private readonly UserManager<Usuario> _userManager;

        public UsuarioPerfilViewComponent(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (currentUser == null)
                return View();

            var model = new UsuarioPerfil(currentUser.UserName, currentUser.Nome, currentUser.Email);
            return View(model);
        }
    }
}
