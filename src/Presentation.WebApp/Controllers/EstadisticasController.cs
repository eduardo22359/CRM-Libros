using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Domain;
using Infrastructure;

namespace Presentation.WebApp.Controllers;

public class EstadisticasController : Controller
{
    private readonly VentasDbContext _ventasDbContext;
    private readonly ClientesDbContext _clientesDbContext;
    private readonly ProductosDbContext _productosDbContext;
    private readonly EmpleadosDbContext _empleadosDbContext;
    public EstadisticasController(IConfiguration configuration)
    {
        _ventasDbContext = new VentasDbContext(configuration.GetConnectionString("DefaultConnection"));
        _clientesDbContext = new ClientesDbContext(configuration.GetConnectionString("DefaultConnection"));
        _productosDbContext = new ProductosDbContext(configuration.GetConnectionString("DefaultConnection"));
        _empleadosDbContext = new EmpleadosDbContext(configuration.GetConnectionString("DefaultConnection"));
    }

    public IActionResult Index()
    {
        var empleados = _empleadosDbContext.List();
        
        ViewBag.Chart1Labels = empleados
            .GroupBy(x => x.Edad)
            .Select(x => $"'{x.Key}'")
            .ToList();
        ViewBag.Chart1Data = empleados
            .GroupBy(x => x.Edad)
            .Select(x => $"{x.Count()}")
            .ToList();

        var clientes = _clientesDbContext.List();
        ViewBag.Chart2Labels = clientes
            .GroupBy(x => x.Nombre)
            .Select(x => $"'{x.Key}'")
            .ToList();
        ViewBag.Chart2Data = clientes
            .GroupBy(x => x.Nombre)
            .Select(x => $"{x.Count()}")
            .ToList();
        return View();
    }


}