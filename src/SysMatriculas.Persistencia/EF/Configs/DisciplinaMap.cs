using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SysMatriculas.Dominio;

namespace SysMatriculas.Persistencia.EF.Configs
{
    public class DisciplinaMap : IEntityTypeConfiguration<Disciplina>
    {
        public void Configure(EntityTypeBuilder<Disciplina> builder)
        {
            builder.ToTable("Disciplinas");

            builder.HasKey(e => e.DisciplinaId);

            builder.Property(e => e.Nome)
                   .IsRequired()
                   .HasMaxLength(100);

            builder
                .HasMany(e => e.DisciplinasOndeEPreRequisito)
                .WithOne(e => e.Disciplina)
                .HasForeignKey(e => e.DisciplinaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(e => e.PreRequisitos)
                .WithOne(e => e.DisciplinaPai)
                .HasForeignKey(e => e.DisciplinaPaiId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(e => e.DisciplinasOndeECoRequisito)
                .WithOne(e => e.Disciplina)
                .HasForeignKey(e => e.DisciplinaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasMany(e => e.CoRequisitos)
               .WithOne(e => e.DisciplinaPai)
               .HasForeignKey(e => e.DisciplinaPaiId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
