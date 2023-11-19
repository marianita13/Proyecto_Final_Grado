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

        builder.HasIndex(e => e.ClientCode, "FK_Orders_Clients_Client");

        builder.HasIndex(e => e.StatusCode, "FK_Orders_Status_Status");

        builder.Property(e => e.Id).HasColumnType("int(11)");
        builder.Property(e => e.ClientCode).HasColumnType("int(11)");
        builder.Property(e => e.Comments).HasMaxLength(500);
        builder.Property(e => e.StatusCode).HasColumnType("int(11)");

        builder.HasOne(d => d.ClientCodeNavigation).WithMany(p => p.Orders)
            .HasForeignKey(d => d.ClientCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Orders_Clients_Client");

        builder.HasOne(d => d.StatusCodeNavigation).WithMany(p => p.Orders)
            .HasForeignKey(d => d.StatusCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Orders_Status_Status");
    }
}
