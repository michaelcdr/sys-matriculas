namespace SysMatriculas.Web.Models
{
    public static class ErrorModel
    {
        public static string Msg
        {
            get {
                return "Ocorreu um erro interno em nossos servidores, " +
                "tente novamente mais tarde.";
            }
        }
    }
}
