﻿using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class MethodPaymentConfiguration : IEntityTypeConfiguration<MethodPayment>
{
    public void Configure(EntityTypeBuilder<MethodPayment> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("method_payment");

        builder.Property(e => e.Id)
            .HasColumnType("int(11)")
            .HasColumnName("id_method");
        builder.Property(e => e.MethodPayment1)
            .HasMaxLength(20)
            .HasColumnName("method_payment");
    }
}
