using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SysMatriculas.Dominio;

namespace SysMatriculas.Persistencia.EF.Configs
{
    public class UsuarioDesempenhoMap : IEntityTypeConfiguration<Desempenho>
    {
        public void Configure(EntityTypeBuilder<Desempenho> builder)
        {
            builder.ToTable("UsuariosDesempenhos");
            builder.HasKey(e => e.UsuarioDesempenhoId);

            builder.HasOne(e => e.Usuario)
                .WithMany(e => e.Desempenhos)
                .HasForeignKey(c => c.UsuarioId);

        }
    }
}
