using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProyectoBiblioteca.Models;
using ProyectoBiblioteca.Data.Configs;

namespace ProyectoBiblioteca.Data.Configs
{
    public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(u => u.idUsuario);

            builder.Property(u => u.nombreCompleto)
                    .HasColumnType("varchar(45)")
                    .HasMaxLength(45)
                    .IsRequired();

            builder.Property(u => u.nombreUsuario)
                    .HasColumnType("varchar(45)")
                    .HasMaxLength(45)
                    .IsRequired();

            builder.HasIndex(u => u.nombreUsuario)
                    .IsUnique()
                    .HasDatabaseName("IX_Usuario_NombreUsuario"); // índice único

            builder.Property(u => u.correo)
                    .HasColumnType("varchar(100)")
                    .IsRequired();

            builder.Property(u => u.contrasena)
                    .HasColumnType("varchar(45)")
                    .HasMaxLength(45)
                    .IsRequired();

            builder.Property(u => u.numeroTelefono)
                    .HasColumnType("varchar(45)")
                    .HasMaxLength(45);
        }
    }
}