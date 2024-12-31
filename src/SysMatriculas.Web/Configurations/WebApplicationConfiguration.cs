using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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