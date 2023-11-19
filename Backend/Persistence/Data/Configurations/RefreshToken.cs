using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class RefreshTokenConf : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("refreshtoken");

            builder.HasIndex(e => e.UserId, "IX_RefreshToken_UserId");

            builder.Property(e => e.Id).HasColumnType("int(11)");
            builder.Property(e => e.Created).HasMaxLength(6);
            builder.Property(e => e.Expires).HasMaxLength(6);
            builder.Property(e => e.UserId).HasColumnType("int(11)");
            builder.Property(e => e.Revoked).HasMaxLength(6);
            builder.Property(e => e.Token).HasMaxLength(50);

            builder.HasOne(d => d.User)
                .WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId);
        }
    }
}