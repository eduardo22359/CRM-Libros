# dotnet-dashboard
dotnet 6 | asp.net mvc | Identity: Individual User Accounts | ef | bootstrap 5 | datatables | chartjs

## Requisitos:
dotnet: 
https://dotnet.microsoft.com/(https://dotnet.microsoft.com/)  
sql server: 
https://www.microsoft.com/es-mx/sql-server/(https://www.microsoft.com/es-mx/sql-server/)  
vs code: 
https://code.visualstudio.com/(https://code.visualstudio.com/)  
libman: 
https://docs.microsoft.com/en-us/aspnet/core/client-side/libman/libman-cli(https://docs.microsoft.com/en-us/aspnet/core/client-side/libman/libman-cli)  
identity: 
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-custom-storage-providers(https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-custom-storage-providers)

## DataBase:
```
CREATE TABLE [Empleado](
    [Id] [uniqueidentifier] PRIMARY KEY NOT NULL DEFAULT(NEWID()),
    [Nombre] [nvarchar](128) NOT NULL,
    [Edad] [tinyint] NOT NULL,
    [Foto] [varchar](max) NULL
);
CREATE TABLE [Producto](
    [Id] [uniqueidentifier] PRIMARY KEY NOT NULL DEFAULT(NEWID()),
    [Descripcion] [nvarchar](128) NOT NULL,
    [Precio] [smallmoney] NOT NULL,
    [Cantidad] [int] NOT NULL DEFAULT(0)
);

CREATE TABLE [Cliente](
    [Id] [uniqueidentifier] PRIMARY KEY NOT NULL DEFAULT(NEWID()),
    [Nombre] [nvarchar](256) NOT NULL,
    [Direccion] [nvarchar](512) NOT NULL,
    [Telefono] [nvarchar](128) NOT NULL,
    [Correo] [nvarchar](256) NOT NULL
);
```

## Arquitecte:
```
.
├───src
│   ├───Application
│   │   └───Services
│   ├───Domain
│   │   ├───Entities
│   │   └───Enums
│   ├───Infrastructure
│   │   ├───Data
│   │   └───Notifications
│   └───Presentation.WebApp
│       ├───Controllers
│       ├───Models
│       ├───Views
│       │   ├───Clientes
│       │   ├───Empleados
│       │   ├───Home
│       │   ├───Productos
│       │   └───Shared
│       └───wwwroot
│           ├───css
│           ├───images
│           ├───js
│           └───lib
└───test
    ├───Application.Test
    ├───Domain.Test
    ├───Infrastructure.Test
    └───Presentation.WebApp.Test
         
```
### #.\src
#### #.\src\Domain
```
dotnet new classlib -o .\src\Domain
rm .\src\Domain\Class1.cs
mkdir .\src\Domain\Entities
echo 'namespace Domain; public class Cliente    { public Guid Id { get; set; } }' > .\src\Domain\Entities\Cliente.cs
echo 'namespace Domain; public class Empleado    { public Guid Id { get; set; } }' > .\src\Domain\Entities\Empleado.cs
echo 'namespace Domain; public class Producto { public Guid Id { get; set; } }' > .\src\Domain\Entities\Producto.cs
```
#### #.\src\Application
```
dotnet new classlib -o .\src\Application

rm .\src\Application\Class1.cs
mkdir .\src\Application\Services
echo 'using System.Drawing; namespace Application; public class FileConverterService { }' > .\src\Application\Services\FileConverterService.cs

dotnet add .\src\Application package System.Drawing.Common

dotnet add .\src\Application reference .\src\Domain
```
#### #.\src\Infrastructure
```
dotnet new classlib -o .\src\Infrastructure

rm .\src\Infrastructure\Class1.cs
mkdir .\src\Infrastructure\Notifications
mkdir .\src\Infrastructure\Data
echo 'using System.Net; using System.Net.Mail; namespace Infrastructure; public class SmtpClientEmailService {}' > .\src\Infrastructure\Notifications\SmtpClientEmailService.cs
echo 'using System.Data; using System.Data.SqlClient; using Domain; namespace Infrastructure; public class ClientesDbContext { }' > .\src\Infrastructure\Data\ClientesDbContext.cs
echo 'using System.Data; using System.Data.SqlClient; using Domain; namespace Infrastructure; public class EmpleadosDbContext { }' > .\src\Infrastructure\Data\EmpleadosDbContext.cs

dotnet add .\src\Infrastructure package System.Data.SqlClient

dotnet add .\src\Infrastructure reference .\src\Application
```
#### #.\src\Presentation
```
dotnet new mvc -o .\src\Presentation.WebApp -au Individual

mkdir .\src\Presentation.WebApp\Views\Clientes
mkdir .\src\Presentation.WebApp\Views\Empleados
mkdir .\src\Presentation.WebApp\Views\Productos
mkdir .\src\Presentation.WebApp\wwwroot\images
echo '<svg></svg>' > .\src\Presentation.WebApp\wwwroot\images\placeholder.svg
echo '<svg></svg>' > .\src\Presentation.WebApp\wwwroot\images\user.svg
echo 'namespace Presentation.WebApp.Controllers { }' > .\src\Presentation.WebApp\Controllers\ClientesController.cs
echo 'namespace Presentation.WebApp.Controllers { }' > .\src\Presentation.WebApp\Controllers\EmpleadosController.cs
echo 'namespace Presentation.WebApp.Controllers { }' > .\src\Presentation.WebApp\Controllers\ProductosController.cs
echo '@{ ViewData["Title"] = "Clientes"; }' >  .\src\Presentation.WebApp\Views\Clientes\Index.cshtml
echo '@{ ViewData["Title"] = "Clientes > Crear"; }' >  .\src\Presentation.WebApp\Views\Clientes\Create.cshtml
echo '@{ ViewData["Title"] = "Clientes > Detalle"; }' >  .\src\Presentation.WebApp\Views\Clientes\Details.cshtml
echo '@{ ViewData["Title"] = "Clientes > Editar"; }' >  .\src\Presentation.WebApp\Views\Clientes\Edit.cshtml
echo '@{ ViewData["Title"] = "Empleados"; }' >  .\src\Presentation.WebApp\Views\Empleados\Index.cshtml
echo '@{ ViewData["Title"] = "Empleados > Crear"; }' >  .\src\Presentation.WebApp\Views\Empleados\Create.cshtml
echo '@{ ViewData["Title"] = "Empleados > Detalle"; }' >  .\src\Presentation.WebApp\Views\Empleados\Details.cshtml
echo '@{ ViewData["Title"] = "Empleados > Editar"; }' >  .\src\Presentation.WebApp\Views\Empleados\Edit.cshtml
echo '@{ ViewData["Title"] = "Productos"; }' >  .\src\Presentation.WebApp\Views\Productos\Index.cshtml
echo '@{ ViewData["Title"] = "Productos > Crear"; }' >  .\src\Presentation.WebApp\Views\Productos\Create.cshtml
echo '@{ ViewData["Title"] = "Productos > Detalle"; }' >  .\src\Presentation.WebApp\Views\Productos\Details.cshtml
echo '@{ ViewData["Title"] = "Productos > Editar"; }' >  .\src\Presentation.WebApp\Views\Productos\Edit.cshtml
echo '{"version": "1.0","defaultProvider": "cdnjs","libraries": []}' > .\src\Presentation.WebApp\libman.json

dotnet remove .\src\Presentation.WebApp package Microsoft.EntityFrameworkCore.Sqlite
dotnet add .\src\Presentation.WebApp package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add .\src\Presentation.WebApp package Microsoft.EntityFrameworkCore.Design
dotnet add .\src\Presentation.WebApp package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add .\src\Presentation.WebApp package Microsoft.AspNetCore.Identity.UI
dotnet add .\src\Presentation.WebApp package Microsoft.EntityFrameworkCore.SqlServer
dotnet add .\src\Presentation.WebApp package Microsoft.EntityFrameworkCore.Tools

dotnet add .\src\Presentation.WebApp reference .\src\Application
dotnet add .\src\Presentation.WebApp reference .\src\Infrastructure
```
```
dotnet aspnet-codegenerator identity -p .\src\Presentation.WebApp -dc Presentation.WebApp.IdentityDbContext --files "Account.Login;Account.Logout;Account.Register;Account.Logout;Account.AccessDenied;Account.Manage._Layout;Account.Manage._ManageNav;Account.Manage._StatusMessage;Account.Manage.ChangePassword;Account.Manage.Index;Account.Manage.SetPassword;"
```
```
# Program.cs
builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;

    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
}).AddEntityFrameworkStores<IdentityDbContext>();
```


### #.\test
#### #.\test\Domain
```
dotnet new xunit -o .\test\Domain.Test
rm .\test\Domain.Test\UnitTest1.cs
dotnet add .\test\Domain.Test reference .\src\Domain
```
#### #.\test\Application
```
dotnet new xunit -o .\test\Application.Test
rm .\test\Application.Test\UnitTest1.cs
dotnet add .\test\Application.Test reference .\src\Application
```
#### #.\test\Infrastructure
```
dotnet new xunit -o .\test\Infrastructure.Test
rm .\test\Infrastructure.Test\UnitTest1.cs
dotnet add .\test\Infrastructure.Test reference .\src\Infrastructure
```
#### #.\test\Presentation
```
dotnet new xunit -o .\test\Presentation.WebApp.Test
rm .\test\Presentation.WebApp.Test\UnitTest1.cs
echo 'using Xunit; namespace Presentation.WebApp.Test { public class RoutingTest { } }' > .\test\Presentation.WebApp.Test\RoutingTest.cs
dotnet add .\test\Presentation.WebApp.Test reference .\src\Presentation.WebApp
```
```
dotnet new sln
dotnet sln add (ls -r .\**\*.csproj)
```

## #Librerias del cliente Web
### #libman.json
```
{
  "version": "1.0",
  "defaultProvider": "cdnjs",
  "libraries": [
    {
      "library": "jquery@#.#.#",
      "destination": "wwwroot/lib/jquery/"
    },
    {
      "library": "jquery-validate@#.#.#",
      "destination": "wwwroot/lib/jquery-validation/"
    },
    {
      "library": "jquery-validation-unobtrusive@#.#.#",
      "destination": "wwwroot/lib/jquery-validation-unobtrusive/"
    },
    {
      "provider": "jsdelivr",
      "library": "bootstrap@#.#.#",
      "destination": "wwwroot/lib/bootstrap/"
    },
    {
      "library": "font-awesome@#.#.#",
      "destination": "wwwroot/lib/font-awesome/"
    },
    {
      "library": "datatables@#.#.#",
      "destination": "wwwroot/lib/datatables/"
    },
    {
      "library": "Chart.js@#.#.#",
      "destination": "wwwroot/lib/chartjs/"
    }
  ]
}
```

```
libman init -p cdnjs
```