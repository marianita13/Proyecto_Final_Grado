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

        builder.HasIndex(e => e.Office, "IX_Employees_Office");

        builder.HasIndex(e => e.PersonId, "IX_Employees_PersonId");

        builder.Property(p => p.OfficeCode).HasColumnType("varchar(255)");


        builder.Property(e => e.Id).HasColumnType("int(11)");
        builder.Property(e => e.Extention).HasMaxLength(50);
        builder.Property(e => e.ManagerCode).HasColumnType("int(11)");
        builder.Property(e => e.PersonId).HasColumnType("int(11)");

        builder.HasOne(d => d.Person).WithMany(p => p.Employees)
            .HasForeignKey(d => d.PersonId)
            .HasConstraintName("FK_Employees_Persons_PersonId");

        builder.HasOne(d => d.Office)
       .WithMany(p => p.Employees)
       .HasForeignKey(o => o.OfficeCode)
       .HasConstraintName("FK_Employees_Offices_OfficeCode");

    }
}
