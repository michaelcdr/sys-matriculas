using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SysMatriculas.Dominio;
using SysMatriculas.Negocio.Services.Interfaces;
using SysMatriculas.Negocio.Services;
using SysMatriculas.Persistencia.EF.Data;
using SysMatriculas.Persistencia.Transacoes;
using SysMatriculas.Web;
using SysMatriculas.Web.Helpers;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<ICurriculoService, CurriculoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IDisciplinaService, DisciplinaService>();
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<ISelectListItemHelper, SelectListItemHelper>();

builder.Services
    .AddControllersWithViews()
   .AddFluentValidation(fv =>
   {
       fv.RegisterValidatorsFromAssemblyContaining<Program>();
   });

var app = builder.Build();
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
//app.UseCookiePolicy();
app.UseAuthentication();


using(var serviceScope = app.Services.CreateScope())
{
    UserManager<Usuario> userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Usuario>>();
    RoleManager<TipoDeUsuario> roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<TipoDeUsuario>>();

    var seedService = new SeedService(userManager, roleManager);
    seedService.Seed();
}

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Curso}/{action=Index}/{id?}"
);

app.Run();