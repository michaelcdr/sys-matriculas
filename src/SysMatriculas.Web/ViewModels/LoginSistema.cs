using System.ComponentModel.DataAnnotations;

namespace SysMatriculas.Web.Models
{
    public class LoginSistema
    {
        [Required(ErrorMessage = "Informe o campo {0}"), Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe o campo {0}"), Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
