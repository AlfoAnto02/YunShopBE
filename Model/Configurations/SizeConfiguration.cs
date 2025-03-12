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
        public void Configure(EntityTypeBuilder<Size> builder) {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.SizeValue)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}
