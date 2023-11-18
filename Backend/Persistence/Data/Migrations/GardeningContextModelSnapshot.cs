﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Data;

#nullable disable

namespace Persistence.Data.Migrations
{
    [DbContext(typeof(GardeningContext))]
    partial class GardeningContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CityName")
                        .HasColumnType("longtext");

                    b.Property<int?>("StateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Domain.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClientName")
                        .HasColumnType("longtext");

                    b.Property<int?>("CodEmployee")
                        .HasColumnType("int");

                    b.Property<int?>("CodEmployeeNavigationId")
                        .HasColumnType("int");

                    b.Property<decimal?>("CreditLimit")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Fax")
                        .HasColumnType("longtext");

                    b.Property<string>("LineAddress")
                        .HasColumnType("longtext");

                    b.Property<string>("LineAddress2")
                        .HasColumnType("longtext");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<int>("PostalCodeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CodEmployeeNavigationId");

                    b.HasIndex("PersonId");

                    b.HasIndex("PostalCodeId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Domain.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CountryName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Domain.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Extention")
                        .HasColumnType("longtext");

                    b.Property<int?>("ManagerCode")
                        .HasColumnType("int");

                    b.Property<string>("OfficeCode")
                        .HasColumnType("longtext");

                    b.Property<string>("OfficeCodeNavigationId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfficeCodeNavigationId");

                    b.HasIndex("PersonId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Domain.Entities.MethodPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("MethodPayment1")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("MethodPayments");
                });

            modelBuilder.Entity("Domain.Entities.Office", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AddressLine1")
                        .HasColumnType("longtext");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<int>("PostalCodeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostalCodeId");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ClientCode")
                        .HasColumnType("int");

                    b.Property<int?>("ClientCodeNavigationId")
                        .HasColumnType("int");

                    b.Property<string>("Comments")
                        .HasColumnType("longtext");

                    b.Property<DateOnly?>("DeliveryDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("ExpectedDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("OrderDate")
                        .HasColumnType("date");

                    b.Property<int>("StatusCode")
                        .HasColumnType("int");

                    b.Property<int?>("StatusCodeNavigationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientCodeNavigationId");

                    b.HasIndex("StatusCodeNavigationId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.Entities.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<short>("LineNumber")
                        .HasColumnType("smallint");

                    b.Property<int>("OrderCode")
                        .HasColumnType("int");

                    b.Property<int?>("OrderCodeNavigationId")
                        .HasColumnType("int");

                    b.Property<string>("ProductCode")
                        .HasColumnType("longtext");

                    b.Property<string>("ProductCodeNavigationId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("OrderCodeNavigationId");

                    b.HasIndex("ProductCodeNavigationId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Domain.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ClientCodeNavigationId")
                        .HasColumnType("int");

                    b.Property<int>("MethodId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("PaymentDate")
                        .HasColumnType("date");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("TransactionId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ClientCodeNavigationId");

                    b.HasIndex("MethodId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Domain.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName1")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName2")
                        .HasColumnType("longtext");

                    b.Property<int>("PersonTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("PostalCodeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonTypeId");

                    b.HasIndex("PostalCodeId");

                    b.ToTable("People");
                });

            modelBuilder.Entity("Domain.Entities.PersonType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("TypeName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("PersonTypes");
                });

            modelBuilder.Entity("Domain.Entities.PostalCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("PostalCode1")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("PostalCodes");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Dimensions")
                        .HasColumnType("longtext");

                    b.Property<int>("IdSupplier")
                        .HasColumnType("int");

                    b.Property<int?>("IdSupplierNavigationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("ProductLine")
                        .HasColumnType("int");

                    b.Property<int?>("ProductLineNavigationId")
                        .HasColumnType("int");

                    b.Property<decimal>("SellingPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<short>("StockQuantity")
                        .HasColumnType("smallint");

                    b.Property<decimal>("SupplierPrice")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("IdSupplierNavigationId");

                    b.HasIndex("ProductLineNavigationId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Domain.Entities.ProductLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DescriptionHtml")
                        .HasColumnType("longtext");

                    b.Property<string>("DescriptionText")
                        .HasColumnType("longtext");

                    b.Property<string>("Image")
                        .HasColumnType("longtext");

                    b.Property<string>("ProductLine1")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ProductLines");
                });

            modelBuilder.Entity("Domain.Entities.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("StateName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("States");
                });

            modelBuilder.Entity("Domain.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("NameStatus")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("Domain.Entities.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Fax")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Dominio.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("Domain.Entities.City", b =>
                {
                    b.HasOne("Domain.Entities.State", "State")
                        .WithMany("Cities")
                        .HasForeignKey("StateId");

                    b.Navigation("State");
                });

            modelBuilder.Entity("Domain.Entities.Client", b =>
                {
                    b.HasOne("Domain.Entities.Employee", "CodEmployeeNavigation")
                        .WithMany("Clients")
                        .HasForeignKey("CodEmployeeNavigationId");

                    b.HasOne("Domain.Entities.Person", "Person")
                        .WithMany("Clients")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.PostalCode", "PostalCode")
                        .WithMany("Clients")
                        .HasForeignKey("PostalCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CodEmployeeNavigation");

                    b.Navigation("Person");

                    b.Navigation("PostalCode");
                });

            modelBuilder.Entity("Domain.Entities.Employee", b =>
                {
                    b.HasOne("Domain.Entities.Office", "OfficeCodeNavigation")
                        .WithMany("Employees")
                        .HasForeignKey("OfficeCodeNavigationId");

                    b.HasOne("Domain.Entities.Person", "Person")
                        .WithMany("Employees")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OfficeCodeNavigation");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Domain.Entities.Office", b =>
                {
                    b.HasOne("Domain.Entities.PostalCode", "PostalCode")
                        .WithMany("Offices")
                        .HasForeignKey("PostalCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PostalCode");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.HasOne("Domain.Entities.Client", "ClientCodeNavigation")
                        .WithMany("Orders")
                        .HasForeignKey("ClientCodeNavigationId");

                    b.HasOne("Domain.Entities.Status", "StatusCodeNavigation")
                        .WithMany("Orders")
                        .HasForeignKey("StatusCodeNavigationId");

                    b.Navigation("ClientCodeNavigation");

                    b.Navigation("StatusCodeNavigation");
                });

            modelBuilder.Entity("Domain.Entities.OrderDetail", b =>
                {
                    b.HasOne("Domain.Entities.Order", "OrderCodeNavigation")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderCodeNavigationId");

                    b.HasOne("Domain.Entities.Product", "ProductCodeNavigation")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductCodeNavigationId");

                    b.Navigation("OrderCodeNavigation");

                    b.Navigation("ProductCodeNavigation");
                });

            modelBuilder.Entity("Domain.Entities.Payment", b =>
                {
                    b.HasOne("Domain.Entities.Client", "ClientCodeNavigation")
                        .WithMany("Payments")
                        .HasForeignKey("ClientCodeNavigationId");

                    b.HasOne("Domain.Entities.MethodPayment", "Method")
                        .WithMany("Payments")
                        .HasForeignKey("MethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientCodeNavigation");

                    b.Navigation("Method");
                });

            modelBuilder.Entity("Domain.Entities.Person", b =>
                {
                    b.HasOne("Domain.Entities.PersonType", "PersonType")
                        .WithMany("People")
                        .HasForeignKey("PersonTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.PostalCode", "PostalCode")
                        .WithMany()
                        .HasForeignKey("PostalCodeId");

                    b.Navigation("PersonType");

                    b.Navigation("PostalCode");
                });

            modelBuilder.Entity("Domain.Entities.PostalCode", b =>
                {
                    b.HasOne("Domain.Entities.City", "City")
                        .WithMany("PostalCodes")
                        .HasForeignKey("CityId");

                    b.Navigation("City");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.HasOne("Domain.Entities.Supplier", "IdSupplierNavigation")
                        .WithMany("Products")
                        .HasForeignKey("IdSupplierNavigationId");

                    b.HasOne("Domain.Entities.ProductLine", "ProductLineNavigation")
                        .WithMany("Products")
                        .HasForeignKey("ProductLineNavigationId");

                    b.Navigation("IdSupplierNavigation");

                    b.Navigation("ProductLineNavigation");
                });

            modelBuilder.Entity("Domain.Entities.State", b =>
                {
                    b.HasOne("Domain.Entities.Country", "Country")
                        .WithMany("States")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Dominio.Entities.RefreshToken", b =>
                {
                    b.HasOne("Domain.Entities.Person", "Persons")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Persons");
                });

            modelBuilder.Entity("Domain.Entities.City", b =>
                {
                    b.Navigation("PostalCodes");
                });

            modelBuilder.Entity("Domain.Entities.Client", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Domain.Entities.Country", b =>
                {
                    b.Navigation("States");
                });

            modelBuilder.Entity("Domain.Entities.Employee", b =>
                {
                    b.Navigation("Clients");
                });

            modelBuilder.Entity("Domain.Entities.MethodPayment", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Domain.Entities.Office", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Domain.Entities.Person", b =>
                {
                    b.Navigation("Clients");

                    b.Navigation("Employees");

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("Domain.Entities.PersonType", b =>
                {
                    b.Navigation("People");
                });

            modelBuilder.Entity("Domain.Entities.PostalCode", b =>
                {
                    b.Navigation("Clients");

                    b.Navigation("Offices");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Domain.Entities.ProductLine", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Domain.Entities.State", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("Domain.Entities.Status", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Domain.Entities.Supplier", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
