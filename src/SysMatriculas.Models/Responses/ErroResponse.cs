namespace SysMatriculas.Dominio.Responses
{
    public class ErroResponse
    {
        public bool Sucesso { get; set; }
        public string Titulo { get; set; }
        public string Css { get; set; }
        public string Mensagem { get; set; }
        public string ExceptionMsg { get; set; }

        public ErroResponse(string message, string exceptionMsg)
        {
            Sucesso = false;
            Titulo = "Erro";
            Mensagem = message;
            Css = "error";
            ExceptionMsg = exceptionMsg;
        }
    }
}
