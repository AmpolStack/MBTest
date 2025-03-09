using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace DbAccess.Models;

public partial class ProducersContext : DbContext
{
    public ProducersContext()
    {
    }

    public ProducersContext(DbContextOptions<ProducersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductOrder> ProductOrders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("Order_id");
            entity.Property(e => e.ClientName)
                .HasMaxLength(100)
                .HasColumnName("client_name");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("payment_method");
            entity.Property(e => e.Total)
                .HasPrecision(10, 2)
                .HasColumnName("total");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("Product_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.SpecificDiscount)
                .HasPrecision(5, 2)
                .HasColumnName("specific_discount");
            entity.Property(e => e.Stock)
                .HasDefaultValueSql("'0'")
                .HasColumnName("stock");
        });

        modelBuilder.Entity<ProductOrder>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.OrderId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("Product_Order");

            entity.HasIndex(e => e.OrderId, "FK_ORDER_ID");

            entity.Property(e => e.ProductId).HasColumnName("Product_id");
            entity.Property(e => e.OrderId).HasColumnName("Order_id");
            entity.Property(e => e.Amount).HasColumnName("amount");

            entity.HasOne(d => d.Order).WithMany(p => p.ProductOrders)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORDER_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductOrders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
