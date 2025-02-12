﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Models;

public partial class asset_managementContext : DbContext
{
    public asset_managementContext(DbContextOptions<asset_managementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<BankAccount> BankAccounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Keyword> Keywords { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<BankAccount>(entity =>
        {
            entity.ToTable("BankAccount");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.BankInistitution)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("bank_inistitution");
            entity.Property(e => e.CardHolder)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("card_holder");
            entity.Property(e => e.CardNumber)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("card_number");
            entity.Property(e => e.ExpiryDate)
                .HasColumnType("date")
                .HasColumnName("expiry_date");
            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("type");

            entity.HasOne(d => d.Account).WithMany(p => p.BankAccounts)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BankAccount_Account");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("categoryName");

            entity.HasOne(d => d.Account).WithMany(p => p.Categories)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Category_Account");
        });

        modelBuilder.Entity<Keyword>(entity =>
        {
            entity.ToTable("Keyword");

            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.KeywordName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("keywordName");

            entity.HasOne(d => d.Account).WithMany(p => p.Keywords)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Keyword_Account");

            entity.HasOne(d => d.Category).WithMany(p => p.Keywords)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Keyword_Category");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("Transaction");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BankAccountId).HasColumnName("bank_accountID");
            entity.Property(e => e.CategoryId).HasColumnName("categoryID");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.IncomingAmount).HasColumnName("incoming_amount");
            entity.Property(e => e.Merchant)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("merchant");
            entity.Property(e => e.OutgoingAmount).HasColumnName("outgoing_amount");

            entity.HasOne(d => d.BankAccount).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.BankAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaction_BankAccount");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}