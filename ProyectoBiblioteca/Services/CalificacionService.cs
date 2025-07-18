using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using ProyectoBiblioteca.Data;
using ProyectoBiblioteca.Models;

namespace ProyectoBiblioteca.Services;

public class CalificacionService
{
    private readonly ProyectoBibliotecaDbContext _context;

    public CalificacionService(ProyectoBibliotecaDbContext context)
    {
        _context = context;
    }

    public async Task AgregarCalificacionAsync(int idLibro, int idUsuario, float calificacion)
    {
        var calificacionNueva = new Calificacion
        {
            idLibro = idLibro,
            idUsuario = idUsuario,
            calificacion = calificacion
        };

        _context.Calificaciones.Add(calificacionNueva);
        await _context.SaveChangesAsync();

        // Actualizar la calificación promedio del libro
        var libro = await _context.Libros
            .Include(l => l.calificaciones)
            .FirstOrDefaultAsync(l => l.idLibro == idLibro);

        if (libro != null)
        {
            var promedio = libro.calificaciones.Average(c => c.calificacion);
            libro.calificacionPromedio = promedio;
            await _context.SaveChangesAsync();
        }
    }
}