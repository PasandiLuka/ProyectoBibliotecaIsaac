using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Components;
namespace ProyectoBiblioteca.Models
{
    public class Calificacion
    {
        public int idCalificacion { get; set; }


        public int idLibro { get; set; }


        public int idUsuario { get; set; }


        public Libro libro { get; set; }


        public Usuario usuario { get; set; }


        [Required(ErrorMessage = "La calificacion es obligatoria.")]
        public float calificacion { get; set; }
    }
}