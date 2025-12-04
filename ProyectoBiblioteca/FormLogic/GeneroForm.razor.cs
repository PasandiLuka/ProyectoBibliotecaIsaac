using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProyectoBiblioteca.Data;
using ProyectoBiblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace ProyectoBiblioteca.FormLogic;

public class GeneroFormBase : ComponentBase
{
    [Inject] IDbContextFactory<ProyectoBibliotecaDbContext> DbFactory { get; set; } = default!;
    //using var db = DbFactory.CreateDbContext();
    protected Genero genero = new();
    protected EditContext editContext = null!;
    protected string mensaje = string.Empty;

    protected override void OnInitialized()
    {
        editContext = new EditContext(genero);
        var store = new ValidationMessageStore(editContext);

        editContext.OnValidationRequested += (s, e) =>
        {
            using var db = DbFactory.CreateDbContext();

            store.Clear();
            if (db.Generos.Any(g => g.genero == genero.genero))
                store.Add(() => genero.genero, "Ese género ya existe");
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

        db.Generos.Add(genero);
        db.SaveChanges();
        mensaje = "Genero registrado correctamente.";
        genero = new Genero();
    }
}