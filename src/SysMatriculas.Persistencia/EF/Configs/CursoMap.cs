using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SysMatriculas.Dominio;

namespace SysMatriculas.Persistencia.EF.Configs
{
    public class CursoMap : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Cursos");
            builder.HasKey(e => e.CursoId);
            builder.Property(e => e.Nome)
                   .IsRequired()
                   .HasMaxLength(100);

            //builder.HasMany(e => e.CursosCurriculos)
            //       .WithOne(e => e.Curso)
            //       .HasForeignKey(e => e.CursoId);

            builder.HasMany(e => e.Curriculos)
                .WithOne(e => e.Curso)
                .HasForeignKey(e => e.CursoId);
                        
        }
    }
}
