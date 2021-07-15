using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SysMatriculas.Dominio;

namespace SysMatriculas.Persistencia.EF.Configs
{
    public class CurriculoMap : IEntityTypeConfiguration<Curriculo>
    {
        public void Configure(EntityTypeBuilder<Curriculo> builder)
        {
            builder.ToTable("Curriculos");
            builder.HasKey(e => e.CurriculoId);
            builder.Property(e => e.Nome)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasOne(e => e.Curso)
                .WithMany(e => e.Curriculos)
                .HasForeignKey(e => e.CursoId);

            builder.HasMany(e => e.Matriculas)
                .WithOne(e => e.Curriculo)
                .HasForeignKey(e => e.CurriculoId);


        }
    }
}
