using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;

namespace Model.Configurations {
    public class ProductConfiguration : IEntityTypeConfiguration<Product> {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnName("Id")
                .IsRequired();
            builder.Property(p => p.Name)
                .HasColumnName("ProductName")
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(p => p.Description)
                .HasColumnName("Description")
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(p => p.Price)
                .HasColumnName("Price")
                .IsRequired();
            builder.Property(p => p.Size)
                .HasColumnName("Size")
                .IsRequired();
            builder.Property(p => p.Brand)
                .HasColumnName("Brand")
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(p => p.CreatedAt)
                .HasColumnName("Created_at")
                .HasColumnType("date");
            builder.Property(p => p.UpdatedAt)
                .HasColumnName("Updated_at")
                .HasColumnType("date");
            builder.Property(p => p.CategoryId)
                .HasColumnName("Category_Id")
                .IsRequired();
            builder.Property(p => p.Stock)
                .HasColumnName("Stock")
                .IsRequired();
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
            builder.HasMany(p => p.Images)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId);
            builder.Property(p => p.UserId)
                .HasColumnName("UserId")
                .IsRequired();
            builder.HasOne(p => p.User)
                .WithMany(u => u.ProductCreated)
                .HasForeignKey(p => p.UserId);
        }

    }
}
