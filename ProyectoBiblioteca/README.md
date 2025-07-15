# ***Se Pico***

## Ahora vamos a unir todo lo que aprendimos:

### Una vez vistos el tema 1 de netcore y el las partes 1 y 2 de RAZOR/Blazor, nos queda enlazar todo, vamos a trabajar bajo el siguiente DER:

```mermaid
erDiagram
    Usuario{
        idUsuario INT PK 
        nombreCompleto VARCHAR(45)
        nombreUsuario VARCHAR(45)
        correo VARCHAR(45)
        contrasena VARCHAR(45)
        numeroTelefono VARCHAR(45)
        dinero DECIMAL(2)
    }
    Editorial{
        idEditorial INT PK
        nombre VARCHAR(45)
        fechaCreacion DATETIME
    }
    Genero{
        idGenero INT PK
        genero VARCHAR(45)
    }
    Libro{
        idLibro INT PK
        idEditorial INT FK
        idGenero INT FK
        idAutor INT FK
        titulo VARCHAR(45)
        fechaCreacion DATETIME
        cantPaginas INT
    }
    Calificaciones{
        idCalificacion INT PK
        idLibro INT FK
        idUsuario INT FK
        calificacion FLOAT
    }
    Comentario{
        idComentario INT PK
        idLibro INT FK
        idUsuario INT FK
        comentario VARCHAR(45)
    }
    Biblioteca{
        idBiblioteca INT PK
        idLibro INT FK
        idUsuario INT FK
    }
    Compra{

    }
    UsuarioCompra{
        
    }
    

    Libro }o--|| Genero: ""
    Libro }o--|| Editorial: ""
    Calificaciones }o--|| Libro: ""
    Comentario }o--|| Libro: ""
    Usuario ||--o{ Comentario: ""
    Usuario ||--o{ Calificaciones: ""
    Biblioteca ||--o{ Usuario: ""
    Biblioteca ||--o{ Libro: ""
```

# 1- Creando el proyecto:

## Creamos un Proyecto Web:

### Para crear un Proyecto Web, Pondremos los siguientes comandos por consola:

```bash
dotnet new web -n nombreDeTuProyecto
cd nombreDeTuProyecto
mkdir wwwroot
mkdir Shared
mkdir Models
mkdir Pages
mkdir Data
```

## Ahora, vamos a seguir los pasos para la creación del Tutorial MiBlazorDesdeCero, la parte 1 y 2

