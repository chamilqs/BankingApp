﻿// <auto-generated />
using System;
using BankingApp.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankingApp.Infrastucture.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BankingApp.Core.Domain.Entities.Beneficiary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SavingsAccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("SavingsAccountId");

                    b.ToTable("Beneficiaries", (string)null);
                });

            modelBuilder.Entity("BankingApp.Core.Domain.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients", (string)null);
                });

            modelBuilder.Entity("BankingApp.Core.Domain.Entities.CreditCard", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<double>("Debt")
                        .HasColumnType("float");

                    b.Property<double>("Limit")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("CreditCards", (string)null);
                });

            modelBuilder.Entity("BankingApp.Core.Domain.Entities.Loan", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Loans", (string)null);
                });

            modelBuilder.Entity("BankingApp.Core.Domain.Entities.SavingsAccount", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsMainAccount")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("SavingsAccounts", (string)null);
                });

            modelBuilder.Entity("BankingApp.Core.Domain.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("Concept")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransactionTypeId");

                    b.ToTable("Transactions", (string)null);
                });

            modelBuilder.Entity("BankingApp.Core.Domain.Entities.TransactionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TransactionTypes", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Account Transfer"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Loan Payment"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Express Payment"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Credit Card Payment"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Cash Advance"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Beneficiary Payment"
                        });
                });

            modelBuilder.Entity("BankingApp.Core.Domain.Entities.Beneficiary", b =>
                {
                    b.HasOne("BankingApp.Core.Domain.Entities.Client", "Client")
                        .WithMany("Beneficiaries")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BankingApp.Core.Domain.Entities.SavingsAccount", "SavingsAccount")
                        .WithMany("Beneficiaries")
                        .HasForeignKey("SavingsAccountId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("SavingsAccount");
                });

            modelBuilder.Entity("BankingApp.Core.Domain.Entities.CreditCard", b =>
                {
                    b.HasOne("BankingApp.Core.Domain.Entities.Client", "Client")
                        .WithMany("CreditCards")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("BankingApp.Core.Domain.Entities.Loan", b =>
                {
                    b.HasOne("BankingApp.Core.Domain.Entities.Client", "Client")
                        .WithMany("Loans")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("BankingApp.Core.Domain.Entities.SavingsAccount", b =>
                {
                    b.HasOne("BankingApp.Core.Domain.Entities.Client", "Client")
                        .WithMany("SavingsAccounts")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("BankingApp.Core.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("BankingApp.Core.Domain.Entities.TransactionType", "TransactionType")
                        .WithMany("Transactions")
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("BankingApp.Core.Domain.Entities.Client", b =>
                {
                    b.Navigation("Beneficiaries");

                    b.Navigation("CreditCards");

                    b.Navigation("Loans");

                    b.Navigation("SavingsAccounts");
                });

            modelBuilder.Entity("BankingApp.Core.Domain.Entities.SavingsAccount", b =>
                {
                    b.Navigation("Beneficiaries");
                });

            modelBuilder.Entity("BankingApp.Core.Domain.Entities.TransactionType", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
