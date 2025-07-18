using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ProyectoBiblioteca.Data;
using ProyectoBiblioteca.Models;
using Microsoft.EntityFrameworkCore;
using ProyectoBiblioteca.Services;

namespace ProyectoBiblioteca.FormLogic;

public class BibliotecaFormBase : ComponentBase
{
    [Inject] IDbContextFactory<ProyectoBibliotecaDbContext> DbFactory { get; set; } = default!;
    //using var db = DbFactory.CreateDbContext();
    [Inject] protected LoginState ls { get; set; } = default!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
    protected EditContext editContext;
    protected List<Libro> Libros;
    protected Biblioteca biblioteca = new Biblioteca();
    protected Dictionary<int, bool> seleccionadosMap = new();
    protected string mensaje = string.Empty;

    protected override void OnInitialized()
    {
        using var db = DbFactory.CreateDbContext();

        // Trae solo los datos necesarios
        Libros = db.Libros.AsNoTracking().ToList();

        // Inicializa el diccionario solo cuando Libros tenga valor
        seleccionadosMap = Libros.ToDictionary(l => l.idLibro, l => false);

        // Crea el EditContext ahora que biblioteca ya existe
        editContext = new EditContext(biblioteca);
    }


    protected void OnCheckChanged(int id, bool seleccionado)
    {
        seleccionadosMap[id] = seleccionado;
        // Opcional: notificar que el campo ha cambiado
        editContext.NotifyFieldChanged(FieldIdentifier.Create(() => seleccionado));
    }


    protected async Task Guardar()
    {
        using var db = DbFactory.CreateDbContext();

        if (!editContext.Validate())
            return;

        var seleccionados = seleccionadosMap
            .Where(kvp => kvp.Value)
            .Select(kvp => kvp.Key)
            .ToList();

        if (!seleccionados.Any())
        {
            mensaje = "Debes seleccionar al menos un libro.";
            return;
        }

        // Crear entidades a insertar en Biblioteca
        var nuevosRegistros = seleccionados
        .Select(id => new Biblioteca
        {
            idUsuario = ls.UsuarioActual,
            idLibro = id
        })
        .ToList();


        db.Bibliotecas.AddRange(nuevosRegistros);
        await db.SaveChangesAsync();


        foreach (var key in seleccionados)
            seleccionadosMap[key] = false;

        editContext.NotifyValidationStateChanged();

        NavigationManager.NavigateTo("/index");
    }
}