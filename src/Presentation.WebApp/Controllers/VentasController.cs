using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;

using Domain;
using Infrastructure;

namespace Presentation.WebApp.Controllers;

public class VentasController : Controller
{
    private readonly VentasDbContext _ventasDbContext;
    private readonly ClientesDbContext _clientesDbContext;

    private readonly ProductosDbContext _productosDbContext;

    private readonly SmtpClientEmailService _emailService;
    public VentasController(IConfiguration configuration)
    {
        _ventasDbContext = new VentasDbContext(configuration.GetConnectionString("DefaultConnection"));
        _clientesDbContext = new ClientesDbContext(configuration.GetConnectionString("DefaultConnection"));
        _productosDbContext = new ProductosDbContext(configuration.GetConnectionString("DefaultConnection"));
        _emailService = new SmtpClientEmailService(configuration.GetSection("Smtp"));

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
        ViewBag.Cliente = _clientesDbContext.List();
        ViewBag.Producto = _productosDbContext.List();

        return View();
    }
    [HttpPost]
    public IActionResult Create(Venta data)
    {
        var cliente = _clientesDbContext.Details(data.ClienteId);
        var producto = _productosDbContext.Details(data.ProductoId);
        _ventasDbContext.Create(data);
        // Envía un Email al Cliente con el detalle de la venta, la cuál contiene Correo, Nombre, Dirección, Teléfono y ID en formato HTML utilizando bootstrap
        _emailService.SendEmail(cliente.Correo, "Detalle de la Venta", $@"
        <div>
          <h1>Detalle de la Venta</h1>
            <hr />
            <p >Fecha y hora de la venta: {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}</p>
          <p>Aquí está la información del cliente:</p>
          <ul>
            <li><strong>ID:</strong> {cliente.Id}</li>
            <li><strong>Nombre:</strong> {cliente.Nombre}</li>
            <li><strong>Correo:</strong> {cliente.Correo}</li>
            <li><strong>Dirección:</strong> {cliente.Direccion}</li>
            <li><strong>Teléfono:</strong> {cliente.Telefono}</li>
          </ul>

          <p>Aquí está la información del producto:</p>
          <ul>
            <li><strong>ID:</strong> {producto.Id}</li>
            <li><strong>Descripción:</strong> {producto.Descripcion}</li>
            <li><strong>Precio:</strong> {producto.Precio.ToString("C")}</li>
          </ul>
        </div>
    ");

        
        return RedirectToAction("Index");
    }



    public IActionResult Edit(Guid id)
    {
        var data = _ventasDbContext.Details(id);
        ViewBag.Cliente = _clientesDbContext.List();
        ViewBag.Producto = _productosDbContext.List();
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