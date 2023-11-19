using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class BossConfiguration: IEntityTypeConfiguration<Boss>
{
    public void Configure(EntityTypeBuilder<Boss> builder)
    {
        builder.ToTable("Boss");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);

        builder.Property(e => e.Extention).HasMaxLength(50);
        builder.Property(e => e.Phone).HasMaxLength(15).IsRequired();

        builder.HasOne(d => d.Person)
            .WithMany(p => p.Bosses)
            .HasForeignKey(d => d.PersonId);

        builder.HasOne(d => d.Office)
            .WithMany(p => p.Bosses)
            .HasForeignKey(o => o.OfficeCode);
    }
}
}