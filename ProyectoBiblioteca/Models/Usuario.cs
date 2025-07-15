using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Components;
using ProyectoBiblioteca.Data;

namespace ProyectoBiblioteca.Models // Cambiá WebBase por tu espacio de nombres
{
    public class Usuario
    {

        [Key]
        public int idUsuario { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string nombreCompleto { get; set; } = string.Empty;

        
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(50)]
        public string nombreUsuario { get; set; } = string.Empty;


        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        public string correo { get; set; } = string.Empty;


        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Debe tener al menos 6 caracteres.")]
        public string contrasena { get; set; } = string.Empty;


        [Phone(ErrorMessage = "Número de teléfono inválido.")]
        [StringLength(20)]
        public string numeroTelefono { get; set; } = string.Empty;


        [Range(0, double.MaxValue, ErrorMessage = "El dinero no puede ser negativo.")]
        public decimal dinero { get; set; }


        public Usuario() {}

    }
}
