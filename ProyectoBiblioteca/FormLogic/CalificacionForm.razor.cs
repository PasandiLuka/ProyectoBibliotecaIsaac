using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProyectoBiblioteca.Data;
using ProyectoBiblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace ProyectoBiblioteca.FormLogic;

public class CalificacionFormBase : ComponentBase
{
    [Inject] IDbContextFactory<ProyectoBibliotecaDbContext> DbFactory { get; set; } = default!;
    //using var db = DbFactory.CreateDbContext();

    protected Calificacion calificacion = new();
    protected EditContext editContext = null!;
    protected string mensaje = string.Empty;

    protected override void OnInitialized()
    {
        editContext = new EditContext(calificacion);
        var store = new ValidationMessageStore(editContext);

        editContext.OnValidationRequested += (s, e) =>
        {
            store.Clear();
            if (calificacion.calificacion < 0 || calificacion.calificacion > 10)
                store.Add(() => calificacion.calificacion, "La calificacion tiene que estar entre 0 y 10.");
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

        db.Calificaciones.Add(calificacion);
        db.SaveChanges();
        mensaje = "Calificacion registrada correctamente.";
        calificacion = new Calificacion();
        OnInitialized();
    }
}