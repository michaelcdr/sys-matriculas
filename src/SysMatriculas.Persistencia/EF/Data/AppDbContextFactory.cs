using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace SysMatriculas.Persistencia.EF.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public IConfiguration Configuration { get; }

        public AppDbContextFactory(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public AppDbContextFactory()
        {

        }

        public AppDbContext CreateDbContext(string[] args)
        {
            string connStr = Configuration["ContextConnection"];
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                        
            optionsBuilder.UseSqlServer(
                connStr,
                obj => obj.MigrationsAssembly(assemblyName)
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
