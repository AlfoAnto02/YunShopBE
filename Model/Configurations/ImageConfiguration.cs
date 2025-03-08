using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;

namespace Model.Configurations {
    public class ImageConfiguration : IEntityTypeConfiguration<Image> {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Image");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").IsRequired();
            builder.Property(x => x.ProductId).HasColumnName("ProductId").IsRequired();
            builder.Property(x => x.Url).HasColumnName("Url").HasMaxLength(255).IsRequired();
            builder.HasOne(x => x.Product).WithMany(x => x.Images).HasForeignKey(x => x.ProductId);
        }

    }
}
