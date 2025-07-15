using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

//Agregado
using ProyectoBiblioteca.Models;

namespace ProyectoBiblioteca.Data
{
    public class ProyectoBibliotecaDbContext : DbContext
    {

        /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Reemplazar con su información
            var connectionString = "server=localhost;database=ProyectoBiblioteca;user=5to_agbd;password=Trigg3rs!;";
            var serverVersion = ServerVersion.Parse("8.0.42");
            //server=localhost;database=ProyectoBiblioteca;user=5to_agbd;password=Trigg3rs!;
            //server=localhost;database=ProyectoBiblioteca;user=root;password=48460731;
            optionsBuilder.UseMySql(connectionString, serverVersion);
        } */

        public ProyectoBibliotecaDbContext(DbContextOptions<ProyectoBibliotecaDbContext> options)
            : base(options) 
        {

        }

        public DbSet<Usuario> Usuario { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración Fluent API para Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.HasKey(u => u.idUsuario);

                entity.Property(u => u.nombreCompleto)
                      .HasColumnType("varchar(45)")
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(u => u.nombreUsuario)
                      .HasColumnType("varchar(45)")
                      .HasMaxLength(50)
                      .IsRequired();

                entity.HasIndex(u => u.nombreUsuario)
                      .IsUnique()
                      .HasDatabaseName("IX_Usuario_NombreUsuario"); // índice único

                entity.Property(u => u.correo)
                      .HasColumnType("varchar(100)")
                      .IsRequired();

                entity.Property(u => u.contrasena)
                      .HasColumnType("varchar(45)")
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(u => u.numeroTelefono)
                      .HasColumnType("varchar(45)")
                      .HasMaxLength(20);

                entity.Property(u => u.dinero)
                      .HasColumnType("decimal(20,2)")
                      .HasPrecision(10, 2);
            });
        }
    }
}

