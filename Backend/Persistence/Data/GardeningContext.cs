using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public partial class GardeningContext : DbContext
{
    public GardeningContext()
    {
    }

    public GardeningContext(DbContextOptions<GardeningContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<MethodPayment> MethodPayments { get; set; }

    public virtual DbSet<Office> Offices { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PersonType> PersonTypes { get; set; }

    public virtual DbSet<PostalCode> PostalCodes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductLine> ProductLines { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=123456;database=gardening", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.27-mariadb"));


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
