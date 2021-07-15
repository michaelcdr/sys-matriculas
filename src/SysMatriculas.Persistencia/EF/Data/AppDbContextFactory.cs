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
            string connStr = Configuration.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            //string connStr = "Data Source=mika-note;Initial Catalog=SysMatriculasDump;User ID=michael;Password=123456;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            
            optionsBuilder.UseSqlServer(
                //"Data Source=MIKA-DESK\\SQLEXPRESS;Initial Catalog=SysMatriculas;User ID=michael;Password=giacom;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", 
                "Data Source=MIKA-DESK\\SQLEXPRESS;Initial Catalog=SysMatriculasVersaoEntregue;User ID=michael;Password=giacom;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
                obj => obj.MigrationsAssembly(assemblyName)
            );
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
