using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

using ProyectoBiblioteca.Data.Configs;
using ProyectoBiblioteca.Data;
using Microsoft.EntityFrameworkCore.Storage;
using ProyectoBiblioteca.Models;
using ProyectoBiblioteca.Services;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


builder.Services.AddDbContextFactory<ProyectoBibliotecaDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//Servicio para habilitar la variable global
builder.Services.AddScoped<LoginState>();

//Servicio para la utilización de la Api (JS Jonson una compañia familiar)
builder.Services.AddHttpClient();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


using (var scope = app.Services.CreateScope())
{
    
    var context = scope.ServiceProvider.GetRequiredService<ProyectoBibliotecaDbContext>();
    /* await context.Database.EnsureDeletedAsync(); */
    var uwu = await context.Database.EnsureCreatedAsync();

    if(uwu)
    {
        var usuarios = new List<Usuario>
        {
            new Usuario { nombreCompleto = "Lucca Pazanddi", nombreUsuario = "LUKITA7956", correo = "lukita@gmail.com", contrasena = "luka1234", numeroTelefono = "1123456789" },
            new Usuario { nombreCompleto = "Sebaz Serpa", nombreUsuario = "SEBITA7956", correo = "sebita@gmail.com", contrasena = "seba1234", numeroTelefono = "1231234324" }
        };

        context.Usuarios.AddRange(usuarios);
        await context.SaveChangesAsync();

        var generos = new List<Genero>
        {
            new Genero { genero = "Fantasia" },
            new Genero { genero = "Clásicos" },
            new Genero { genero = "Infantil" },
            new Genero { genero = "Distopico" },
            new Genero { genero = "Aventura" }
        };

        context.Generos.AddRange(generos);
        await context.SaveChangesAsync();

        var nuevosLibros = new List<Libro>
        {
            new Libro { idGenero = 1, titulo = "Cien años de soledad", editorial = "Sudamericana", autor = "Gabriel García Márquez", fechaCreacion = new DateTime(1967, 6, 5), cantidadPaginas = 417 },
            new Libro { idGenero = 2, titulo = "Don Quijote de la Mancha", editorial = "Francisco de Robles", autor = "Miguel de Cervantes", fechaCreacion = new DateTime(1605, 1, 16), cantidadPaginas = 863 },
            new Libro { idGenero = 3, titulo = "El Principito", editorial = "Reynal & Hitchcock", autor = "Antoine de Saint-Exupéry", fechaCreacion = new DateTime(1943, 4, 6), cantidadPaginas = 96 },
            new Libro { idGenero = 1, titulo = "La casa de los espíritus", editorial = "Plaza & Janés", autor = "Isabel Allende", fechaCreacion = new DateTime(1982, 1, 1), cantidadPaginas = 448 },
            new Libro { idGenero = 4, titulo = "1984", editorial = "Secker & Warburg", autor = "George Orwell", fechaCreacion = new DateTime(1949, 6, 8), cantidadPaginas = 328 },
            new Libro { idGenero = 2, titulo = "Hamlet", editorial = "Nicholas Ling", autor = "William Shakespeare", fechaCreacion = new DateTime(1603, 7, 26), cantidadPaginas = 160 },
            new Libro { idGenero = 3, titulo = "Matar a un ruiseñor", editorial = "J.B. Lippincott & Co.", autor = "Harper Lee", fechaCreacion = new DateTime(1960, 7, 11), cantidadPaginas = 281 },
            new Libro { idGenero = 5, titulo = "El señor de los anillos", editorial = "Allen & Unwin", autor = "J.R.R. Tolkien", fechaCreacion = new DateTime(1954, 7, 29), cantidadPaginas = 1178 },
            new Libro { idGenero = 4, titulo = "Fahrenheit 451", editorial = "Ballantine Books", autor = "Ray Bradbury", fechaCreacion = new DateTime(1953, 10, 19), cantidadPaginas = 194 },
            new Libro { idGenero = 5, titulo = "Crimen y castigo", editorial = "The Russian Messenger", autor = "Fiódor Dostoievski", fechaCreacion = new DateTime(1866, 1, 1), cantidadPaginas = 671 },
            new Libro { idGenero = 1, titulo = "Rayuela", editorial = "Rayuela", autor = "Julio Cortázar", fechaCreacion = new DateTime(1963, 6, 28), cantidadPaginas = 736 },
            new Libro { idGenero = 2, titulo = "La odisea", editorial = "Ancient Greece Press", autor = "Homero", fechaCreacion = new DateTime(700, 1, 1), cantidadPaginas = 500 },
            new Libro { idGenero = 3, titulo = "Orgullo y prejuicio", editorial = "T. Egerton", autor = "Jane Austen", fechaCreacion = new DateTime(1813, 1, 28), cantidadPaginas = 279 },
            new Libro { idGenero = 4, titulo = "Brave New World", editorial = "Chatto & Windus", autor = "Aldous Huxley", fechaCreacion = new DateTime(1932, 8, 30), cantidadPaginas = 311 },
            new Libro { idGenero = 5, titulo = "Guerra y paz", editorial = "The Russian Messenger", autor = "León Tolstói", fechaCreacion = new DateTime(1869, 1, 1), cantidadPaginas = 1225 },
            new Libro { idGenero = 1, titulo = "La sombra del viento", editorial = "Planeta", autor = "Carlos Ruiz Zafón", fechaCreacion = new DateTime(2001, 4, 1), cantidadPaginas = 487 },
            new Libro { idGenero = 2, titulo = "Macbeth", editorial = "Thomas Creede", autor = "William Shakespeare", fechaCreacion = new DateTime(1606, 1, 1), cantidadPaginas = 85 },
            new Libro { idGenero = 3, titulo = "El amor en los tiempos del cólera", editorial = "Oveja Negra", autor = "Gabriel García Márquez", fechaCreacion = new DateTime(1985, 4, 5), cantidadPaginas = 348 },
            new Libro { idGenero = 4, titulo = "Animal Farm", editorial = "Secker & Warburg", autor = "George Orwell", fechaCreacion = new DateTime(1945, 8, 17), cantidadPaginas = 112 },
            new Libro { idGenero = 5, titulo = "Los hermanos Karamázov", editorial = "The Russian Messenger", autor = "Fiódor Dostoievski", fechaCreacion = new DateTime(1880, 1, 1), cantidadPaginas = 824 }
        };

        context.Libros.AddRange(nuevosLibros);
        await context.SaveChangesAsync();
        
        var nuevosPrestamos = new List<Biblioteca>
        {
            new Biblioteca {idUsuario = 1, idLibro = 1},
            new Biblioteca {idUsuario = 1, idLibro = 2},
            new Biblioteca {idUsuario = 1, idLibro = 3},
            new Biblioteca {idUsuario = 1, idLibro = 4},
            new Biblioteca {idUsuario = 1, idLibro = 5}
        };

        context.Bibliotecas.AddRange(nuevosPrestamos);
        await context.SaveChangesAsync();
    }
}

app.Run();