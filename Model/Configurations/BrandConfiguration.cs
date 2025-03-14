using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;

namespace Model.Configurations {
    public class BrandConfiguration : IEntityTypeConfiguration<Brand> {
        public void Configure(EntityTypeBuilder<Brand> builder) {
            builder.ToTable("Brand");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name)
                .HasColumnName("BrandName")
                .IsRequired()
                .HasMaxLength(50);
            builder.HasOne(b => b.AddedByUser)
                .WithMany(u => u.BrandCreated)
                .HasForeignKey(b => b.AddedBy);
            builder.Property(b => b.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();
            builder.Property(b => b.UpdatedAt)
                .HasColumnName("UpdatedAt")
                .IsRequired();
        }
    }
}
