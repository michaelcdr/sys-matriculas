using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SysMatriculas.Negocio.Services;
using SysMatriculas.Persistencia.EF.Data;
using SysMatriculas.Persistencia.Seed;

namespace SysMatriculas.Web.Configurations;

public static class WebApplicationConfiguration
{
    public static WebApplication ConfigurarWebApp(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCookiePolicy();
        app.UseAuthentication();
        

       
        using (var serviceScope = app.Services.CreateScope())
        {

            var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

            context.Database.EnsureCreated();

            //UserManager<Usuario> userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Usuario>>();
            //RoleManager<TipoDeUsuario> roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<TipoDeUsuario>>();
            var seedService = serviceScope.ServiceProvider.GetRequiredService<ISeedService>();
            
            seedService.Seed();
        }

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Curso}/{action=Index}/{id?}"
        );

        app.Run();

        return app;
    }
}