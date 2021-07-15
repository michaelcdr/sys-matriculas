namespace SysMatriculas.Dominio.Responses
{
    public class SucessoResponse
    {
        public bool sucesso { get; set; }
        public string titulo { get; set; }
        public string mensagem { get; set; }
        public string css { get; set; }

        public SucessoResponse(string mensagem)
        {
            sucesso = true;
            titulo = "Sucesso";
            this.mensagem = mensagem;
            css = "success";
        }          
    }
}
