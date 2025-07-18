using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProyectoBiblioteca.Data;
using ProyectoBiblioteca.Models;
using Microsoft.EntityFrameworkCore;


namespace ProyectoBiblioteca.FormLogic;

public class UsuarioFormBase : ComponentBase
{
    [Inject] IDbContextFactory<ProyectoBibliotecaDbContext> DbFactory { get; set; } = default!;
    //using var db = DbFactory.CreateDbContext();
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
    protected Usuario usuario = new();
    protected EditContext editContext = null!;

    protected override void OnInitialized()
    {
        using var db = DbFactory.CreateDbContext();

        editContext = new EditContext(usuario);
        var store = new ValidationMessageStore(editContext);

        editContext.OnValidationRequested += (s, e) =>
        {
            store.Clear();
            if (db.Usuarios.Any(u => u.nombreUsuario == usuario.nombreUsuario))
                store.Add(() => usuario.nombreUsuario, "El nombre de usuario ya existe.");
            editContext.NotifyValidationStateChanged();
        };

        editContext.OnFieldChanged += (s, e) =>
        {
            store.Clear(e.FieldIdentifier);
            editContext.NotifyValidationStateChanged();
        };
    }

    protected async void Guardar()
    {
        using var db = DbFactory.CreateDbContext();

        if (!editContext.Validate()) return;

        db.Usuarios.Add(usuario);
        await db.SaveChangesAsync();
        usuario = new Usuario();
        await OnInitializedAsync();
        NavigationManager.NavigateTo("/");

    }
}