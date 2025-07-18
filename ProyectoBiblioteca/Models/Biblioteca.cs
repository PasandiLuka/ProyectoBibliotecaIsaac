using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Components;
using Org.BouncyCastle.Asn1.Icao;

namespace ProyectoBiblioteca.Models;

public class Biblioteca
{
    public int idLibro { get; set; }
    public int? idUsuario { get; set; }

    public Libro Libros { get; set; }

    public Usuario Usuarios { get; set; }
}

