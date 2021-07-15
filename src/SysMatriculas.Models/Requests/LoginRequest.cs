namespace SysMatriculas.Dominio
{
    public class LoginRequest
    {
        public LoginRequest(string userName, string senha)
        {
            UserName = userName;
            Senha = senha;
        }

        public string UserName { get; set; }
        public string Senha { get; set; }
    }
}
