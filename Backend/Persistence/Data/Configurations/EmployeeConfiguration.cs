using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {

        builder.ToTable("Employees");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);

        builder.Property(e => e.Extention).HasMaxLength(50);

        builder.HasOne(d => d.Person)
            .WithMany(p => p.Employees)
            .HasForeignKey(d => d.PersonId);

        builder.HasOne(d => d.Office)
            .WithMany(p => p.Employees)
            .HasForeignKey(o => o.OfficeCode);

    }
}
