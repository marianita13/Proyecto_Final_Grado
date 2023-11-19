using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(e => new { e.Id, e.TransactionId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            builder.ToTable("payments");

            builder.HasIndex(e => e.MethodId, "IX_Payments_MethodId");

            builder.Property(e => e.Id).HasColumnType("int(11)");
            builder.Property(e => e.TransactionId).HasMaxLength(50);
            builder.Property(e => e.MethodId).HasColumnType("int(11)");

            builder.HasOne(d => d.IdNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Clients_Client");

            builder.HasOne(d => d.Method).WithMany(p => p.Payments)
                .HasForeignKey(d => d.MethodId)
                .HasConstraintName("FK_Payments_MethodPayments_MethodId");
    }
}
