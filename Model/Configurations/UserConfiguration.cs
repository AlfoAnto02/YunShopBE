using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;

namespace Model.Configurations {
    public class UserConfiguration : IEntityTypeConfiguration<User> {
        public void Configure(EntityTypeBuilder<User> builder) {
            builder.ToTable("User");

            // Chiave primaria
            builder.HasKey(u => u.Id);

            // Configurazione delle proprietà
            builder.Property(u => u.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(u => u.Username)
                .HasColumnName("Username")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnName("Email")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Password)
                .HasColumnName("Password")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Role)
                .HasColumnName("Role")
                .HasMaxLength(50);

            builder.Property(u => u.Session_Id)
                .HasColumnName("Session_id")
                .HasMaxLength(50);

            builder.Property(u => u.CreatedAt)
                .HasColumnName("Creation_Date")
                // Se vuoi usare solo la parte di data, in EF Core 6+ puoi usare DateOnly.
                // Altrimenti, con DateTime, specifica il tipo SQL "date" per memorizzare solo la data.
                .HasColumnType("date");

            builder.Property(u => u.UpdatedAt)
                .HasColumnName("Update_Date")
                .HasColumnType("date");

            builder.Property(u => u.Cart_Id)
                .HasColumnName("Cart_id");

            builder.Property(u => u.Phone)
                .HasColumnName("Phone_number")
                .HasMaxLength(50);
        }
    }
}
