using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProyectoBiblioteca.Data;
using ProyectoBiblioteca.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace ProyectoBiblioteca.FormLogic;

public class LibroFormBase : ComponentBase
{
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
    [Inject] IDbContextFactory<ProyectoBibliotecaDbContext> DbFactory { get; set; } = default!;
    //using var db = DbFactory.CreateDbContext();
    protected Libro libro = new();
    protected EditContext editContext = null!;
    protected string mensaje = string.Empty;

    protected override void OnInitialized()
    {
        editContext = new EditContext(libro);
        var store = new ValidationMessageStore(editContext);

        editContext.OnValidationRequested += (s, e) =>
        {
            using var db = DbFactory.CreateDbContext();
            store.Clear();
            if (db.Libros.Any(l => l.titulo == libro.titulo))
                store.Add(() => libro.titulo, "Ese titulo ya existe.");
            editContext.NotifyValidationStateChanged();
        };

        editContext.OnFieldChanged += (s, e) =>
        {
            store.Clear(e.FieldIdentifier);
            editContext.NotifyValidationStateChanged();
        };
    }

    protected void Guardar()
    {
        using var db = DbFactory.CreateDbContext();
        if (!editContext.Validate()) return;
        libro.fechaCreacion = DateTime.Now;
        db.Libros.Add(libro);
        db.SaveChanges();
        mensaje = "Libro Creado con Exito";
        libro = new Libro();
        NavigationManager.NavigateTo("/libro");
    }
}