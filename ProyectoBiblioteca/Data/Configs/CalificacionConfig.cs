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
    public class CalificacionConfig : IEntityTypeConfiguration<Calificacion>
    {
        public void Configure(EntityTypeBuilder<Calificacion> builder)
        {
            builder.ToTable("Calificacion");

            builder.HasKey(c => c.idCalificacion);

            builder.HasOne(c => c.libro)
                   .WithMany(l => l.calificaciones) // <- especifica la colección en Libro
                   .HasForeignKey(l => l.idLibro)
                   .IsRequired();

            builder.HasOne(c => c.usuario)
                   .WithMany(u => u.calificaciones)  // <- especifica la colección en Usuario
                   .HasForeignKey(u => u.idUsuario)
                   .IsRequired();

            builder.Property(c => c.calificacion)
                   .HasColumnType("float")
                   .IsRequired();
        }
    }
}