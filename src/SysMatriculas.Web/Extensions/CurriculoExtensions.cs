using SysMatriculas.Dominio;
using SysMatriculas.Dominio.Requests;
using SysMatriculas.Dominio.Responses;

namespace SysMatriculas.Web.Extensions
{
    public static class CurriculoExtensions
    {
        public static CurriculoRequest ToCurriculoRequest(this Curriculo curriculo)
        {
            return new CurriculoRequest(curriculo.CurriculoId, curriculo.Nome);
        }

        public static CurriculoResponse ToCurriculoResponse(this Curriculo curriculo)
        {
            return new CurriculoResponse(curriculo.CurriculoId, curriculo.Nome);
        }
    }
}
