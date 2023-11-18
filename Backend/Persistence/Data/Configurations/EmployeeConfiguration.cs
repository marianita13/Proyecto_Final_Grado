using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("employee");

        builder.HasIndex(e => e.OfficeCode, "office_code");

        builder.HasIndex(e => e.PersonId, "person_id");

        builder.Property(e => e.Id)
            .HasColumnType("int(11)")
            .HasColumnName("employee_code");
        builder.Property(e => e.Extention)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnName("extention");
        builder.Property(e => e.ManagerCode)
            .HasColumnType("int(11)")
            .HasColumnName("manager_code");
        builder.Property(e => e.OfficeCode)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnName("office_code");
        builder.Property(e => e.PersonId)
            .HasColumnType("int(11)")
            .HasColumnName("person_id");

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
