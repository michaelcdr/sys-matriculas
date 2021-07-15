using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SysMatriculas.Dominio;

namespace SysMatriculas.Persistencia.EF.Configs
{

    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.Property(e => e.UserName)
                    .HasColumnName("Login");

            builder.Property(e => e.NormalizedUserName)
                    .HasColumnName("LoginNormalizado");

            builder.Property(e => e.PhoneNumber)
                .HasColumnName("Telefone");

            builder.Property(e => e.PasswordHash)
                .HasColumnName("Senha");

            builder.Property(e => e.EmailConfirmed)
                .HasColumnName("EmailConfirmacao");

            builder.Property(e => e.PhoneNumberConfirmed)
                .HasColumnName("TelefoneConfirmacao");

            builder.Property(e => e.Id)
                .HasColumnName("UsuarioId");

            builder.HasMany(e => e.Matriculas)
                .WithOne(e => e.Usuario)
                .HasForeignKey(e => e.UsuarioId);

            builder.HasMany(e => e.Desempenhos)
                .WithOne(e => e.Usuario)
                .HasForeignKey(e => e.UsuarioId);
        }
    }
}
