using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SysMatriculas.Negocio.Services.Interfaces;
using SysMatriculas.Negocio.Services;
using SysMatriculas.Persistencia.Transacoes;
using SysMatriculas.Web.Helpers;
using SysMatriculas.Persistencia.Seed;

namespace SysMatriculas.Web.Configurations;

public static class ServicosConfiguration
{
    public static WebApplicationBuilder ConfigurarServicos(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISeedService, SeedService>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<ICursoService, CursoService>();
        builder.Services.AddScoped<ICurriculoService, CurriculoService>();
        builder.Services.AddScoped<IUsuarioService, UsuarioService>();
        builder.Services.AddScoped<IDisciplinaService, DisciplinaService>();
        builder.Services.AddScoped<IAlunoService, AlunoService>();
        builder.Services.AddScoped<ISelectListItemHelper, SelectListItemHelper>();

        return builder;
    }
}
