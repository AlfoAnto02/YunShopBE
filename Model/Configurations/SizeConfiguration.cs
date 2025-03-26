using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;

namespace Model.Configurations {
    public class SizeConfiguration : IEntityTypeConfiguration<Size> {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.ToTable("Size");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.SizeValue)
                .HasColumnName("SizeValue")
                .IsRequired()
                .HasMaxLength(10);
            builder.HasOne(s => s.User)
                .WithMany(u => u.SizeCreated)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
