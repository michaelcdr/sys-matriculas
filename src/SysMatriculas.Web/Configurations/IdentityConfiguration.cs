using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SysMatriculas.Dominio;
using SysMatriculas.Persistencia.EF.Data;

namespace SysMatriculas.Web.Configurations
{
    public static class IdentityConfiguration
    {
        public static WebApplicationBuilder ConfigurarIdentity(this WebApplicationBuilder builder)
        {
            //configurando autenticação com identity
            builder.Services.AddIdentity<Usuario, TipoDeUsuario>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddRoleManager<RoleManager<TipoDeUsuario>>()
                    .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;

                // User settings
                options.User.RequireUniqueEmail = false;

            });

            builder.Services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/Usuario/Login";
                option.AccessDeniedPath = "/Usuario/Login";
                option.Cookie.Name = "SysMatriculas";
            });

            builder.Services.AddAuthorization();
            return builder;
        }
    }
}
