using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
    _ventasDbContext.Create(data);
    var cliente = _clientesDbContext.Details(data.ClienteId);
    var producto = _productosDbContext.Details(data.ProductoId);

    // Envía un Email al Cliente con el detalle de la venta, la cuál contiene Correo, Nombre, Dirección, Teléfono y ID en formato HTML utilizando bootstrap
    _emailService.SendEmail(cliente.Correo, "Detalle de la Venta", $@"
<div class='container'>
  <div class='row'>
    <div class='col-md-6'>
      <h1>Detalle de la Venta</h1>
        <hr />
        <p class='lead'>Fecha y hora de la venta: {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}</p>
      <p class='lead'>Aquí está la información del cliente:</p>
      <ul class='list-group'>
        <li class='list-group-item'><strong>ID:</strong> {cliente.Id}</li>
        <li class='list-group-item'><strong>Nombre:</strong> {cliente.Nombre}</li>
        <li class='list-group-item'><strong>Correo:</strong> {cliente.Correo}</li>
        <li class='list-group-item'><strong>Dirección:</strong> {cliente.Direccion}</li>
        <li class='list-group-item'><strong>Teléfono:</strong> {cliente.Telefono}</li>
      </ul>

      <p class='lead'>Aquí está la información del producto:</p>
      <ul class='list-group'>
        <li class='list-group-item'><strong>ID:</strong> {producto.Id}</li>
        <li class='list-group-item'><strong>Descripción:</strong> {producto.Descripcion}</li>
        <li class='list-group-item'><strong>Precio:</strong> {producto.Precio.ToString("C")}</li>
      </ul>
    </div>
  </div>
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