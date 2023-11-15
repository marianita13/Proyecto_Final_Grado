using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(e => new { e.ClientCode, e.TransactionId })
            .HasName("PRIMARY")
            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

        builder.ToTable("payment");

        builder.HasIndex(e => e.MethodId, "method_id");

        builder.Property(e => e.ClientCode)
            .HasColumnType("int(11)")
            .HasColumnName("client_code");
        builder.Property(e => e.TransactionId)
            .HasMaxLength(50)
            .HasColumnName("transaction_id");
        builder.Property(e => e.MethodId)
            .HasColumnType("int(11)")
            .HasColumnName("method_id");
        builder.Property(e => e.PaymentDate).HasColumnName("payment_date");
        builder.Property(e => e.Total)
            .HasPrecision(15, 2)
            .HasColumnName("total");

        builder.HasOne(d => d.ClientCodeNavigation).WithMany(p => p.Payments)
            .HasForeignKey(d => d.ClientCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("payment_ibfk_2");

        builder.HasOne(d => d.Method).WithMany(p => p.Payments)
            .HasForeignKey(d => d.MethodId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("payment_ibfk_1");
    }
}
