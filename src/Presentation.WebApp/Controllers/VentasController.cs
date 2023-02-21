using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Domain;
using Infrastructure;

namespace Presentation.WebApp.Controllers;

public class VentasController : Controller
{
    private readonly VentasDbContext _ventasDbContext;
    public VentasController(IConfiguration configuration)
    {
        _ventasDbContext = new VentasDbContext(configuration.GetConnectionString("DefaultConnection"));
    }

    public IActionResult Index()
    {
        var data = _ventasDbContext.List();
        return View(data);
    }

    public IActionResult Details(Guid id)
    {
        var data = _ventasDbContext.Details(id);
        return View(data);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Venta data)
    {
        _ventasDbContext.Create(data);
        //SmtpClientEmailService.SendEmail(data.Cliente.Correo, "Asunto", $"<h4>Hola {data.Cliente.Nombre}</h1>", true);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(Guid id)
    {
        var data = _ventasDbContext.Details(id);
        return View(data);
    }
    [HttpPost]
    public IActionResult Edit(Venta data)
    {
        _ventasDbContext.Edit(data);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(Guid id)
    {
        _ventasDbContext.Delete(id);
        return RedirectToAction("Index");
    }
}