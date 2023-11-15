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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=123456;database=gardening", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.27-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<City>(entity =>
        {
            
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientCode).HasName("PRIMARY");

            entity.ToTable("client");

            entity.HasIndex(e => e.PersonId, "person_id");

            entity.Property(e => e.ClientCode)
                .HasColumnType("int(11)")
                .HasColumnName("client_code");
            entity.Property(e => e.CreditLimit)
                .HasPrecision(15, 2)
                .HasColumnName("credit_limit");
            entity.Property(e => e.Fax)
                .HasMaxLength(15)
                .HasColumnName("fax");
            entity.Property(e => e.PersonId)
                .HasColumnType("int(11)")
                .HasColumnName("person_id");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");

            entity.HasOne(d => d.Person).WithMany(p => p.Clients)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("client_ibfk_1");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PRIMARY");

            entity.ToTable("country");

            entity.Property(e => e.CountryId)
                .HasColumnType("int(11)")
                .HasColumnName("country_id");
            entity.Property(e => e.CountryName)
                .HasMaxLength(50)
                .HasColumnName("country_name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeCode).HasName("PRIMARY");

            entity.ToTable("employee");

            entity.HasIndex(e => e.OfficeCode, "office_code");

            entity.HasIndex(e => e.PersonId, "person_id");

            entity.Property(e => e.EmployeeCode)
                .HasColumnType("int(11)")
                .HasColumnName("employee_code");
            entity.Property(e => e.ManagerCode)
                .HasColumnType("int(11)")
                .HasColumnName("manager_code");
            entity.Property(e => e.OfficeCode)
                .HasMaxLength(10)
                .HasColumnName("office_code");
            entity.Property(e => e.PersonId)
                .HasColumnType("int(11)")
                .HasColumnName("person_id");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .HasColumnName("position");

            entity.HasOne(d => d.OfficeCodeNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.OfficeCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_ibfk_2");

            entity.HasOne(d => d.Person).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_ibfk_1");
        });

        modelBuilder.Entity<MethodPayment>(entity =>
        {
            entity.HasKey(e => e.IdMethod).HasName("PRIMARY");

            entity.ToTable("method_payment");

            entity.Property(e => e.IdMethod)
                .HasColumnType("int(11)")
                .HasColumnName("id_method");
            entity.Property(e => e.MethodPayment1)
                .HasMaxLength(20)
                .HasColumnName("method_payment");
        });

        modelBuilder.Entity<Office>(entity =>
        {
            entity.HasKey(e => e.OfficeCode).HasName("PRIMARY");

            entity.ToTable("office");

            entity.HasIndex(e => e.PostalCodeId, "postal_code_id");

            entity.Property(e => e.OfficeCode)
                .HasMaxLength(10)
                .HasColumnName("office_code");
            entity.Property(e => e.AddressLine1)
                .HasMaxLength(50)
                .HasColumnName("address_line1");
            entity.Property(e => e.AddressLine2)
                .HasMaxLength(50)
                .HasColumnName("address_line2");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.PostalCodeId)
                .HasColumnType("int(11)")
                .HasColumnName("postal_code_id");

            entity.HasOne(d => d.PostalCode).WithMany(p => p.Offices)
                .HasForeignKey(d => d.PostalCodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("office_ibfk_1");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderCode).HasName("PRIMARY");

            entity.ToTable("orders");

            entity.HasIndex(e => e.ClientCode, "client_code");

            entity.HasIndex(e => e.StatusCode, "status_code");

            entity.Property(e => e.OrderCode)
                .HasColumnType("int(11)")
                .HasColumnName("order_code");
            entity.Property(e => e.ClientCode)
                .HasColumnType("int(11)")
                .HasColumnName("client_code");
            entity.Property(e => e.Comments)
                .HasColumnType("text")
                .HasColumnName("comments");
            entity.Property(e => e.DeliveryDate).HasColumnName("delivery_date");
            entity.Property(e => e.ExpectedDate).HasColumnName("expected_date");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.StatusCode)
                .HasColumnType("int(11)")
                .HasColumnName("status_code");

            entity.HasOne(d => d.ClientCodeNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ClientCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_ibfk_2");

            entity.HasOne(d => d.StatusCodeNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_ibfk_1");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderCode, e.ProductCode })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("order_detail");

            entity.HasIndex(e => e.ProductCode, "product_code");

            entity.Property(e => e.OrderCode)
                .HasColumnType("int(11)")
                .HasColumnName("order_code");
            entity.Property(e => e.ProductCode)
                .HasMaxLength(15)
                .HasColumnName("product_code");
            entity.Property(e => e.LineNumber)
                .HasColumnType("smallint(6)")
                .HasColumnName("line_number");
            entity.Property(e => e.Quantity)
                .HasColumnType("int(11)")
                .HasColumnName("quantity");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(15, 2)
                .HasColumnName("unit_price");

            entity.HasOne(d => d.OrderCodeNavigation).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_detail_ibfk_1");

            entity.HasOne(d => d.ProductCodeNavigation).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_detail_ibfk_2");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => new { e.ClientCode, e.TransactionId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("payment");

            entity.HasIndex(e => e.MethodId, "method_id");

            entity.Property(e => e.ClientCode)
                .HasColumnType("int(11)")
                .HasColumnName("client_code");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(50)
                .HasColumnName("transaction_id");
            entity.Property(e => e.MethodId)
                .HasColumnType("int(11)")
                .HasColumnName("method_id");
            entity.Property(e => e.PaymentDate).HasColumnName("payment_date");
            entity.Property(e => e.Total)
                .HasPrecision(15, 2)
                .HasColumnName("total");

            entity.HasOne(d => d.ClientCodeNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ClientCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_ibfk_2");

            entity.HasOne(d => d.Method).WithMany(p => p.Payments)
                .HasForeignKey(d => d.MethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_ibfk_1");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PRIMARY");

            entity.ToTable("person");

            entity.HasIndex(e => e.PersonTypeId, "person_type_id");

            entity.HasIndex(e => e.PostalCodeId, "postal_code_id");

            entity.Property(e => e.PersonId)
                .HasColumnType("int(11)")
                .HasColumnName("person_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Extension)
                .HasMaxLength(10)
                .HasColumnName("extension");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName1)
                .HasMaxLength(50)
                .HasColumnName("last_name1");
            entity.Property(e => e.LastName2)
                .HasMaxLength(50)
                .HasColumnName("last_name2");
            entity.Property(e => e.PersonTypeId)
                .HasColumnType("int(11)")
                .HasColumnName("person_type_id");
            entity.Property(e => e.PostalCodeId)
                .HasColumnType("int(11)")
                .HasColumnName("postal_code_id");

            entity.HasOne(d => d.PersonType).WithMany(p => p.People)
                .HasForeignKey(d => d.PersonTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_ibfk_1");

            entity.HasOne(d => d.PostalCode).WithMany(p => p.People)
                .HasForeignKey(d => d.PostalCodeId)
                .HasConstraintName("person_ibfk_2");
        });

        modelBuilder.Entity<PersonType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PRIMARY");

            entity.ToTable("person_type");

            entity.Property(e => e.TypeId)
                .HasColumnType("int(11)")
                .HasColumnName("type_id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<PostalCode>(entity =>
        {
            entity.HasKey(e => e.PostalCodeId).HasName("PRIMARY");

            entity.ToTable("postal_code");

            entity.HasIndex(e => e.CityId, "city_id");

            entity.Property(e => e.PostalCodeId)
                .HasColumnType("int(11)")
                .HasColumnName("postal_code_id");
            entity.Property(e => e.CityId)
                .HasColumnType("int(11)")
                .HasColumnName("city_id");
            entity.Property(e => e.PostalCode1)
                .HasMaxLength(10)
                .HasColumnName("postal_code");

            entity.HasOne(d => d.City).WithMany(p => p.PostalCodes)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("postal_code_ibfk_1");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductCode).HasName("PRIMARY");

            entity.ToTable("product");

            entity.HasIndex(e => e.IdSupplier, "id_supplier");

            entity.HasIndex(e => e.ProductLine, "product_line");

            entity.Property(e => e.ProductCode)
                .HasMaxLength(15)
                .HasColumnName("product_code");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Dimensions)
                .HasMaxLength(25)
                .HasColumnName("dimensions");
            entity.Property(e => e.IdSupplier)
                .HasColumnType("int(11)")
                .HasColumnName("id_supplier");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .HasColumnName("name");
            entity.Property(e => e.ProductLine)
                .HasColumnType("int(11)")
                .HasColumnName("product_line");
            entity.Property(e => e.SellingPrice)
                .HasPrecision(15, 2)
                .HasColumnName("selling_price");
            entity.Property(e => e.StockQuantity)
                .HasColumnType("smallint(6)")
                .HasColumnName("stock_quantity");
            entity.Property(e => e.SupplierPrice)
                .HasPrecision(15, 2)
                .HasColumnName("supplier_price");

            entity.HasOne(d => d.IdSupplierNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdSupplier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_ibfk_1");

            entity.HasOne(d => d.ProductLineNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductLine)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_ibfk_2");
        });

        modelBuilder.Entity<ProductLine>(entity =>
        {
            entity.HasKey(e => e.CodProductLine).HasName("PRIMARY");

            entity.ToTable("product_line");

            entity.Property(e => e.CodProductLine)
                .HasColumnType("int(11)")
                .HasColumnName("cod_product_line");
            entity.Property(e => e.DescriptionHtml)
                .HasColumnType("text")
                .HasColumnName("description_html");
            entity.Property(e => e.DescriptionText)
                .HasColumnType("text")
                .HasColumnName("description_text");
            entity.Property(e => e.Image)
                .HasMaxLength(256)
                .HasColumnName("image");
            entity.Property(e => e.ProductLine1)
                .HasMaxLength(50)
                .HasColumnName("product_line");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("PRIMARY");

            entity.ToTable("state");

            entity.HasIndex(e => e.CountryId, "country_id");

            entity.Property(e => e.StateId)
                .HasColumnType("int(11)")
                .HasColumnName("state_id");
            entity.Property(e => e.CountryId)
                .HasColumnType("int(11)")
                .HasColumnName("country_id");
            entity.Property(e => e.StateName)
                .HasMaxLength(50)
                .HasColumnName("state_name");

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("state_ibfk_1");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.CodStatus).HasName("PRIMARY");

            entity.ToTable("status");

            entity.Property(e => e.CodStatus)
                .HasColumnType("int(11)")
                .HasColumnName("cod_status");
            entity.Property(e => e.NameStatus)
                .HasMaxLength(20)
                .HasColumnName("name_status");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PRIMARY");

            entity.ToTable("supplier");

            entity.Property(e => e.SupplierId)
                .HasColumnType("int(11)")
                .HasColumnName("supplier_id");
            entity.Property(e => e.Fax)
                .HasMaxLength(15)
                .HasColumnName("fax");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
