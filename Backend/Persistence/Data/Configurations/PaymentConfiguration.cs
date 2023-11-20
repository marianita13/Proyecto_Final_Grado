using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
            builder.ToTable("payments");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(e => e.TransactionId).HasMaxLength(50);
            builder.Property(e => e.MethodId).HasColumnType("int(11)");

            builder.HasOne(d => d.IdNavigation)
                .WithMany(p => p.Payments)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Method)
                .WithMany(p => p.Payments)
                .HasForeignKey(d => d.MethodId);
    }
}
