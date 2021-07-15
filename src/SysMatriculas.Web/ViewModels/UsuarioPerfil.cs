namespace SysMatriculas.Web.ViewModels
{
    public class UsuarioPerfil
    {
        public UsuarioPerfil(string userName, string nome, string email)
        {
            UserName = userName;
            Nome = nome;
            Email = email;
        }

        public string UserName { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
