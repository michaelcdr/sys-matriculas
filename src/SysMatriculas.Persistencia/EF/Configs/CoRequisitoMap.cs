using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SysMatriculas.Dominio;

namespace SysMatriculas.Persistencia.EF.Configs
{
    public class CoRequisitoMap : IEntityTypeConfiguration<CoRequisito>
    {
        public void Configure(EntityTypeBuilder<CoRequisito> builder)
        {
            builder.ToTable("CoRequisitos");

            builder.HasKey(e => e.CoRequisitosId);

        }
    }
}
