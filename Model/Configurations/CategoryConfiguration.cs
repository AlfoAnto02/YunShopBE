using Microsoft.EntityFrameworkCore;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.Configurations {
    public class CategoryConfiguration : IEntityTypeConfiguration<Category> {
        public void Configure(EntityTypeBuilder<Category> builder) {
            builder.ToTable("Category");

            // Chiave primaria
            builder.HasKey(c => c.Id);

            // Configurazione delle proprietà
            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(c => c.AddedById)
                .HasColumnName("AddedBy")
                .IsRequired();

            builder.Property(c => c.Name)
                .HasColumnName("CategoryName")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .HasColumnName("Created_at")
                .HasColumnType("date");

            builder.Property(c => c.UpdatedAt)
                .HasColumnName("Updated_at")
                .HasColumnType("date");

            builder.HasOne(c => c.AddedBy)
                .WithMany(c => c.CategoryCreated)
                .HasForeignKey(c => c.AddedById);


        }
        
    }
}
