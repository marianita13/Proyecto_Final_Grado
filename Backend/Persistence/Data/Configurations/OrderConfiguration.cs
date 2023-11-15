using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("orders");

        builder.HasIndex(e => e.ClientCode, "client_code");

        builder.HasIndex(e => e.StatusCode, "status_code");

        builder.Property(e => e.Id)
            .HasColumnType("int(11)")
            .HasColumnName("order_code");
        builder.Property(e => e.ClientCode)
            .HasColumnType("int(11)")
            .HasColumnName("client_code");
        builder.Property(e => e.Comments)
            .HasColumnType("text")
            .HasColumnName("comments");
        builder.Property(e => e.DeliveryDate).HasColumnName("delivery_date");
        builder.Property(e => e.ExpectedDate).HasColumnName("expected_date");
        builder.Property(e => e.OrderDate).HasColumnName("order_date");
        builder.Property(e => e.StatusCode)
            .HasColumnType("int(11)")
            .HasColumnName("status_code");

        builder.HasOne(d => d.ClientCodeNavigation).WithMany(p => p.Orders)
            .HasForeignKey(d => d.ClientCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("orders_ibfk_2");

        builder.HasOne(d => d.StatusCodeNavigation).WithMany(p => p.Orders)
            .HasForeignKey(d => d.StatusCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("orders_ibfk_1");
    }
}
