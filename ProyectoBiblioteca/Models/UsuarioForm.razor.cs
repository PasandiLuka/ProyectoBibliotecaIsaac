/* using Microsoft.AspNetCore.Components;
using ProyectoBiblioteca.Models;
using ProyectoBiblioteca.Data;

namespace ProyectoBiblioteca.Models;

public class UsuarioFormBase : ComponentBase
{
    protected Usuario usuario = new();
    protected string mensaje = "";

    protected void Guardar()
    {
        using (var context = new ProyectoBibliotecaDbContext())
        {
            context.Usuario.Add(usuario);
            context.SaveChanges();
        }

        mensaje = $"Usuario {usuario.nombreUsuario} registrado correctamente.";
        usuario = new(); // reset
    }
} */

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProyectoBiblioteca.Data;
using ProyectoBiblioteca.Models;

namespace ProyectoBiblioteca.Pages
{
    public class UsuarioFormBase : ComponentBase
    {
        [Inject] protected ProyectoBibliotecaDbContext db { get; set; } = default!;
        protected Usuario usuario = new();
        protected EditContext editContext = null!;
        protected string mensaje = string.Empty;

        protected override void OnInitialized()
        {
            /* editContext = new EditContext(usuario);
            editContext.OnValidationRequested += ValidarUnicidad; */
            editContext = new EditContext(usuario);
            var store = new ValidationMessageStore(editContext);

            editContext.OnValidationRequested += (s, e) => {
            store.Clear();
            if (db.Usuario.Any(u => u.nombreUsuario == usuario.nombreUsuario))
                store.Add(() => usuario.nombreUsuario, "El nombre de usuario ya existe.");
                editContext.NotifyValidationStateChanged();
            };

            editContext.OnFieldChanged += (s, e) => {
                store.Clear(e.FieldIdentifier);
                editContext.NotifyValidationStateChanged();
            };
        }

        /* private void ValidarUnicidad(object _, ValidationRequestedEventArgs __)
        {
            
            var store = new ValidationMessageStore(editContext);
            store.Clear();
            if (db.Usuario.Any(u => u.nombreUsuario == usuario.nombreUsuario))
            {
                store.Clear();
                store.Add(() => usuario.nombreUsuario, "El nombre de usuario ya existe.");
            }
            editContext.NotifyValidationStateChanged();
        } */

        protected void Guardar()
        {
            if (!editContext.Validate()) return;

            db.Usuario.Add(usuario);
            db.SaveChanges();
            mensaje = "Usuario registrado correctamente.";
            usuario = new Usuario();
            OnInitialized();
        }
    }
}