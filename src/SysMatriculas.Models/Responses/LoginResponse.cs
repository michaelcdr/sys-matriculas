namespace SysMatriculas.Dominio.Responses
{
    public class LoginResponse
    {
        public LoginResponse(bool logado, string tipoDeUsuario, string resultado)
        {
            Logado = logado;
            TipoDeUsuario = tipoDeUsuario;
            Resultado = resultado;
        }

        public bool Logado { get; set; }
        public string TipoDeUsuario { get; set; }
        public string Resultado { get; set; }
    }
}
