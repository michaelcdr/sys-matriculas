using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SysMatriculas.Dominio;
using SysMatriculas.Negocio.Services;
using SysMatriculas.Negocio.Services.Interfaces;
using SysMatriculas.Persistencia.EF.Data;
using SysMatriculas.Persistencia.Transacoes;
using SysMatriculas.Web.Helpers;
using SysMatriculas.Web.Models;
using SysMatriculas.Web.Validators;
using SysMatriculas.Web.ViewModels;
using System.Reflection;

namespace SysMatriculas.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Esse metodo e onde adicionaremos nossa referencias para o container de injeção de dependencias.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            string connStr = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(
                    connStr,
                    obj => obj.MigrationsAssembly(assemblyName)
                );
            });

            //configurando autenticação com identity
            services.AddIdentity<Usuario, TipoDeUsuario>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddRoleManager<RoleManager<TipoDeUsuario>>()
                    .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
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

            services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/Usuario/Login";
                option.AccessDeniedPath = "/Usuario/Login";
                option.Cookie.Name = "SysMatriculas";
            });

            services.AddAuthorization();

            services.AddTransient<IValidator<UsuarioCadastro>, UsuarioValidator>();
            services.AddTransient<IValidator<DisciplinaCadastroViewModel>, DisciplinaValidator>();
            services.AddTransient<IValidator<DisciplinaEdicaoViewModel>, DisciplinaValidator>();
            services.AddTransient<IValidator<CursoViewModel>, CursoValidator>();
            services.AddTransient<IValidator<Curriculo>, CurriculoValidator>();
            services.AddTransient<IValidator<AlunoCadastroViewModel>, AlunoValidator>();
            services.AddTransient<IValidator<AlunoEditViewModel>, AlunoEditValidator>();
            services.AddTransient<IValidator<AssociacaoAlunoComCurso>, MatriculaValidator>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICursoService, CursoService>();
            services.AddScoped<ICurriculoService, CurriculoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IDisciplinaService, DisciplinaService>();
            services.AddScoped<IAlunoService, AlunoService>();
            services.AddScoped<ISelectListItemHelper, SelectListItemHelper>();

            services
                .AddControllersWithViews()
                .AddFluentValidation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                              IHostingEnvironment env, 
                              UserManager<Usuario> userManager,
                              RoleManager<TipoDeUsuario> roleManager
            )
        {
            if (env.IsDevelopment())
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

            var seedService = new SeedService(userManager, roleManager);
            seedService.Seed();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Curso}/{action=Index}/{id?}");
            });
        }
    }
}
