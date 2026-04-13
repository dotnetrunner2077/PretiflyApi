# PretiflyAPI - Arquitectura CQRS

## Estructura de Proyectos

La solución está organizada en las siguientes capas:

### 1. **PretiflyAPI.Domain** (.NET 9)
- Contiene las entidades del dominio
- Ubicación: `PretiflyAPI.Domain/Entities/`
- Entidades scaffoldeadas desde la base de datos MySQL:
  - CategoriesXContent
  - Category
  - Client
  - Content
  - ContentLanguage
  - ContentLocation
  - ContentsType
  - Country
  - Language
  - MembershipsClient
  - MembershipsType
  - Subtitle
  - View

### 2. **PretiflyAPI.Infrastructure** (.NET 9)
- Contiene la implementación de acceso a datos
- DbContext: `PretiflyAPI.Infrastructure.Data.PretiflyDbContext`
- Paquetes instalados:
  - Pomelo.EntityFrameworkCore.MySql (9.0.0)
  - Microsoft.EntityFrameworkCore.Design (9.0.0)
  - Microsoft.EntityFrameworkCore.Tools (9.0.0)

### 3. **PretiflyAPI.Application** (.NET 9)
- Contiene la lógica de negocio separada en Commands y Queries (CQRS)
- Interfaces base para CQRS en `Common/Interfaces/`:
  - `IQuery<TResult>`
  - `ICommand` y `ICommand<TResult>`
  - `IQueryHandler<TQuery, TResult>`
  - `ICommandHandler<TCommand>` y `ICommandHandler<TCommand, TResult>`
  - `IPretiflyDbContext`

#### Ejemplo de Query (GetAllContents):
- Query: `PretiflyAPI.Application/Contents/Queries/GetAllContentsQuery.cs`
- Handler: `PretiflyAPI.Application/Contents/Queries/GetAllContentsQueryHandler.cs`

### 4. **PretiflyAPI** (.NET 10)
- Proyecto Web API principal
- Controladores en `Controllers/`
- Configuración de inyección de dependencias en `Program.cs`
- **Swagger UI** habilitado para documentación de la API
  - Acceso en desarrollo: `https://localhost:{port}/swagger`

## Swagger/OpenAPI

La API está documentada con Swagger UI, que proporciona una interfaz interactiva para explorar y probar los endpoints.

### Acceder a Swagger
En modo desarrollo, accede a: `https://localhost:{puerto}/swagger`

### Configuración
La configuración de Swagger está en `Program.cs`:

```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ...

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

### Paquetes instalados
- Swashbuckle.AspNetCore (10.1.7)

## Configuración de Base de Datos

### Connection String
La cadena de conexión está configurada en `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=pretifly;User=root;Password=123456;"
  }
}
```

### Configuración en Program.cs

El DbContext está registrado con MySQL:

```csharp
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));

builder.Services.AddDbContext<PretiflyDbContext>(options =>
    options.UseMySql(connectionString, serverVersion));
```

## Patrón CQRS

### Crear una Query

1. Crear la clase Query que implemente `IQuery<TResult>`:
```csharp
public class GetAllContentsQuery : IQuery<List<Content>>
{
}
```

2. Crear el Handler que implemente `IQueryHandler<TQuery, TResult>`:
```csharp
public class GetAllContentsQueryHandler : IQueryHandler<GetAllContentsQuery, List<Content>>
{
    private readonly PretiflyDbContext _context;

    public GetAllContentsQueryHandler(PretiflyDbContext context)
    {
        _context = context;
    }

    public List<Content> Handle(GetAllContentsQuery query)
    {
        return _context.Contents.ToList();
    }
}
```

3. Registrar el Handler en `Program.cs`:
```csharp
builder.Services.AddScoped<GetAllContentsQueryHandler>();
```

4. Usar en el Controller:
```csharp
[ApiController]
[Route("api/[controller]")]
public class ContentsController : ControllerBase
{
    private readonly GetAllContentsQueryHandler _getAllContentsHandler;

    public ContentsController(GetAllContentsQueryHandler getAllContentsHandler)
    {
        _getAllContentsHandler = getAllContentsHandler;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var query = new GetAllContentsQuery();
        var contents = _getAllContentsHandler.Handle(query);
        return Ok(contents);
    }
}
```

### Crear un Command

1. Crear la clase Command que implemente `ICommand` o `ICommand<TResult>`:
```csharp
public class CreateContentCommand : ICommand<int>
{
    public string Title { get; set; }
    public int? ReleaseYear { get; set; }
}
```

2. Crear el Handler que implemente `ICommandHandler<TCommand, TResult>`:
```csharp
public class CreateContentCommandHandler : ICommandHandler<CreateContentCommand, int>
{
    private readonly PretiflyDbContext _context;

    public CreateContentCommandHandler(PretiflyDbContext context)
    {
        _context = context;
    }

    public int Handle(CreateContentCommand command)
    {
        var content = new Content
        {
            Title = command.Title,
            ReleaseYear = command.ReleaseYear
        };
        
        _context.Contents.Add(content);
        _context.SaveChanges();
        
        return content.IdContents;
    }
}
```

3. Registrar el Handler en `Program.cs`
4. Usar en el Controller

## Referencias entre Proyectos

- **PretiflyAPI** → PretiflyAPI.Application
- **PretiflyAPI** → PretiflyAPI.Infrastructure
- **PretiflyAPI.Application** → PretiflyAPI.Domain
- **PretiflyAPI.Application** → PretiflyAPI.Infrastructure
- **PretiflyAPI.Infrastructure** → PretiflyAPI.Domain

## Scaffold de Base de Datos

Para regenerar las entidades desde la base de datos:

```powershell
cd PretiflyAPI.Infrastructure
dotnet ef dbcontext scaffold "Server=Server_Name;Port=3306;Database=pretifly;User=User_Name;Password=Password;" Pomelo.EntityFrameworkCore.MySql --context PretiflyDbContext --context-dir Data --output-dir ../PretiflyAPI.Domain/Entities --force
```

Después de hacer scaffold, cambiar los namespaces:
```powershell
Get-ChildItem -Path PretiflyAPI.Domain\Entities\*.cs | ForEach-Object { 
    (Get-Content $_) -replace 'namespace PretiflyAPI.Infrastructure;', 'namespace PretiflyAPI.Domain.Entities;' | Set-Content $_ 
}
```

## Notas Importantes

- **PretiflyAPI.Domain**, **PretiflyAPI.Infrastructure** y **PretiflyAPI.Application** usan .NET 9.0 debido a la compatibilidad con Pomelo.EntityFrameworkCore.MySql 9.0
- **PretiflyAPI** (proyecto principal) usa .NET 10.0
- dotnet-ef tool versión 9.0.0 está instalado globalmente para compatibilidad con EF Core 9
