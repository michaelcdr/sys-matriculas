using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SysMatriculas.Persistencia.EF.Data;
using System.IO;
using System.Reflection;
using FluentValidation.AspNetCore;

namespace SysMatriculas.Web.Configurations
{
    public static class MvcConfiguration
    {
        public static WebApplicationBuilder ConfigurarMvc(this WebApplicationBuilder builder)
        {
            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Program>();

            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            string connStr = builder.Configuration["ConnStr"];

            builder.Services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(
                    connStr,
                    obj => obj.MigrationsAssembly(assemblyName)
                );
            });

            builder.Services.AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<Program>();
            }).AddControllersWithViews();
            return builder;
        }
    }
}