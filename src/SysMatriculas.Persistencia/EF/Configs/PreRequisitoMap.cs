using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SysMatriculas.Dominio;

namespace SysMatriculas.Persistencia.EF.Configs
{
    public class PreRequisitoMap : IEntityTypeConfiguration<PreRequisito>
    {
        public void Configure(EntityTypeBuilder<PreRequisito> builder)
        {
            builder.ToTable("PreRequisitos");

            builder.HasKey(e => e.PreRequisitosId);

            //builder
            //    .HasOne(e => e.DisciplinaBase)
            //    .WithMany(e => e.PreRequisitos)
            //    .HasForeignKey(e => e.DisciplinaId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder
            //    .HasOne(e => e.DisciplinaPreRequisito)
            //    .WithMany(e => e.PreRequisitos)
            //    .HasForeignKey(e => e.DisciplinaPreRequisitoId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
