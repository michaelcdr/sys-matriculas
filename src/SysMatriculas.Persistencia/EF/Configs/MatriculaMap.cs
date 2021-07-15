using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SysMatriculas.Dominio;

namespace SysMatriculas.Persistencia.EF.Configs
{
    public class MatriculaMap : IEntityTypeConfiguration<Matricula>
    {
        public void Configure(EntityTypeBuilder<Matricula> builder)
        {
            builder.ToTable("Matriculas");
            builder.HasKey(e => e.MatriculaId);

            builder.HasOne(e => e.Curriculo)
                .WithMany(e => e.Matriculas)
                .HasForeignKey(e => e.CurriculoId);

            builder.HasOne(e => e.Usuario)
                .WithMany(e => e.Matriculas)
                .HasForeignKey(e => e.UsuarioId);

        }
    }
}
