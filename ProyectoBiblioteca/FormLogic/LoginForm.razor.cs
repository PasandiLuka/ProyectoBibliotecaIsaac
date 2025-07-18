using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProyectoBiblioteca.Data;

using ProyectoBiblioteca.Models;
using Microsoft.EntityFrameworkCore;
using ProyectoBiblioteca.Services;

namespace ProyectoBiblioteca.FormLogic;

public class LoginFormBase : ComponentBase
{
    [Inject] IDbContextFactory<ProyectoBibliotecaDbContext> DbFactory { get; set; } = default!;
    //using var db = DbFactory.CreateDbContext();
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
    [Inject] protected LoginState ls { get; set; } = default!;
    protected EditContext editContext = null!;
    private ValidationMessageStore messageStore;
    protected string usuario { get; set; } = "";
    protected string contrasena { get; set; } = "";
    protected string mensaje = string.Empty;

    
    protected override void OnInitialized()
    {
        editContext = new EditContext(this);
        messageStore = new ValidationMessageStore(editContext);
        editContext.OnFieldChanged += (s, e) =>
        {
            // Al cambiar cualquier campo, limpia sus mensajes
            messageStore.Clear(e.FieldIdentifier);
        };
    }


    protected async Task HandleLogin()
    {
        using var db = DbFactory.CreateDbContext();

        if (!editContext.Validate()) return;

        var usr = await db.Usuarios
                          .AsNoTracking()
                          .FirstOrDefaultAsync(u => u.nombreUsuario == usuario);

        if (usr != null && usr.contrasena == contrasena)
        {
            ls.SetValor(usr.idUsuario);
            NavigationManager.NavigateTo("/index");
        }
        else
        {
            // Limpiar solo contraseña
            contrasena = "";
            messageStore.Clear(editContext.Field(nameof(contrasena)));
            editContext.NotifyValidationStateChanged();
            StateHasChanged();
            mensaje = "Usuario o contraseña incorrectos.";
        }
    }
}