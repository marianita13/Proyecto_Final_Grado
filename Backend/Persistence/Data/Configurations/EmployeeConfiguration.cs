using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.EmployeeCode).HasName("PRIMARY");

        builder.ToTable("employee");

        builder.HasIndex(e => e.OfficeCode, "office_code");

        builder.HasIndex(e => e.PersonId, "person_id");

        builder.Property(e => e.EmployeeCode)
            .HasColumnType("int(11)")
            .HasColumnName("employee_code");
        builder.Property(e => e.ManagerCode)
            .HasColumnType("int(11)")
            .HasColumnName("manager_code");
        builder.Property(e => e.OfficeCode)
            .HasMaxLength(10)
            .HasColumnName("office_code");
        builder.Property(e => e.PersonId)
            .HasColumnType("int(11)")
            .HasColumnName("person_id");
        builder.Property(e => e.Position)
            .HasMaxLength(50)
            .HasColumnName("position");

        builder.HasOne(d => d.OfficeCodeNavigation).WithMany(p => p.Employees)
            .HasForeignKey(d => d.OfficeCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("employee_ibfk_2");

        builder.HasOne(d => d.Person).WithMany(p => p.Employees)
            .HasForeignKey(d => d.PersonId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("employee_ibfk_1");
    }
}
