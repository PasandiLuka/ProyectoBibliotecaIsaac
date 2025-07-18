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
    public class GeneroConfig : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder.ToTable("Genero");

            builder.HasKey(g => g.idGenero);

            builder.Property(g => g.genero)
                   .HasColumnType("varchar(45)")
                   .HasMaxLength(45)
                   .IsRequired();

            builder.HasIndex(g => g.genero)
                    .IsUnique()
                    .HasDatabaseName("IX_Genero_genero");
        }
    }
}