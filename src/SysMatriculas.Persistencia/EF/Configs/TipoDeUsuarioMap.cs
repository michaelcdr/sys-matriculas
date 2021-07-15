using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SysMatriculas.Dominio;

namespace SysMatriculas.Persistencia.EF.Configs
{
    public class TipoDeUsuarioMap : IEntityTypeConfiguration<TipoDeUsuario>
    {
        public void Configure(EntityTypeBuilder<TipoDeUsuario> builder)
        {
            builder.ToTable("TiposDeUsuarios");

        }
    }
}
