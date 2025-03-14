using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;

namespace Model.Configurations {
    public class ProductSizeConfiguration : IEntityTypeConfiguration<ProductSize> {
        public void Configure(EntityTypeBuilder<ProductSize> builder) {
            builder.HasKey(ps => ps.Id);
            builder.HasOne(ps => ps.Product)
                .WithMany(p => p.ProductSizes)
                .HasForeignKey(ps => ps.ProductId);
            builder.HasOne(ps => ps.Size)
                .WithMany(s => s.ProductSizes)
                .HasForeignKey(ps => ps.SizeId);
            builder.Property(ps => ps.Stock)
                .HasColumnName("Stock")
                .IsRequired();
            builder.Property(ps => ps.Price)
                .HasColumnName("Price")
                .IsRequired();
        }
    }
}
