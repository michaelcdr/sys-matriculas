using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SysMatriculas.Dominio;
using SysMatriculas.Persistencia.EF.Configs;

namespace SysMatriculas.Persistencia.EF.Data
{
    public class AppDbContext : IdentityDbContext<Usuario, TipoDeUsuario, string>
    {
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Curriculo> Curriculos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
       

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoDeUsuario> TipoDeUsuarios { get; set; }
        public DbSet<PreRequisito> PreRequisitos { get; set; }
        public DbSet<CoRequisito> CoRequisitos { get; set; }
        public DbSet<Desempenho> Desempenhos { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }

        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CursoMap());
            builder.ApplyConfiguration(new CurriculoMap());
            builder.ApplyConfiguration(new DisciplinaMap());
            //builder.ApplyConfiguration(new CursoCurriculoMap());
            //builder.ApplyConfiguration(new CurriculoDisciplinaMap());

            builder.ApplyConfiguration(new UsuarioMap());
            builder.ApplyConfiguration(new TipoDeUsuarioMap());            
            builder.ApplyConfiguration(new CoRequisitoMap());
            builder.ApplyConfiguration(new PreRequisitoMap());
            builder.ApplyConfiguration(new MatriculaMap());
            builder.ApplyConfiguration(new UsuarioDesempenhoMap());
        }
    }
}
