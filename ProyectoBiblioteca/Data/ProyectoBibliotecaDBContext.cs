using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
//Agregado
using ProyectoBiblioteca.Models;
using ProyectoBiblioteca.Data.Configs;
using ProyectoBiblioteca.Data;
//server=localhost;database=ProyectoBiblioteca;user=5to_agbd;password=Trigg3rs!;
//server=localhost;database=ProyectoBiblioteca;user=root;password=48460731;

namespace ProyectoBiblioteca.Data
{
      public class ProyectoBibliotecaDbContext : DbContext
      {
            public ProyectoBibliotecaDbContext(DbContextOptions<ProyectoBibliotecaDbContext> options)
                  : base(options) {}

            public DbSet<Usuario> Usuarios { get; set; }
            public DbSet<Calificacion> Calificaciones { get; set; }
            public DbSet<Libro> Libros { get; set; }
            public DbSet<Genero> Generos { get; set; }
            public DbSet<Biblioteca> Bibliotecas { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                  modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProyectoBibliotecaDbContext).Assembly);
            }
      }
}

