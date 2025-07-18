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
    public class BibliotecaConfig : IEntityTypeConfiguration<Biblioteca>
    {
        public void Configure(EntityTypeBuilder<Biblioteca> builder)
        {
            builder.ToTable("Biblioteca");

            builder.HasKey(b => new { b.idLibro, b.idUsuario });

            builder.HasOne(l => l.Libros);
            builder.HasOne(u => u.Usuarios);
        }
    }
}