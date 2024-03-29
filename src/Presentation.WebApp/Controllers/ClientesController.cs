using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Domain;
using Infrastructure;

namespace Presentation.WebApp.Controllers;
public class ClientesController : Controller
{
    private readonly ClientesDbContext _clientesDbContext;
    public ClientesController(IConfiguration configuration)
    {
        _clientesDbContext = new ClientesDbContext(configuration.GetConnectionString("DefaultConnection"));
    }
    public IActionResult Index()
    {
        var data = _clientesDbContext.List();
        return View(data);
    }

    //[Authorize]
    public IActionResult Details(Guid id)
    {
        var data = _clientesDbContext.Details(id);
        return View(data);
    }

    //[Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    //[Authorize(Roles = "Admin")]
    public IActionResult Create(Cliente data, IFormFile file)
    {
        _clientesDbContext.Create(data);
        return RedirectToAction("Index");
    }

    //[Authorize(Roles = "Admin")]
    public IActionResult Edit(Guid id)
    {
        var data = _clientesDbContext.Details(id);
        return View(data);
    }
    [HttpPost]
    //[Authorize(Roles = "Admin")]
    public IActionResult Edit(Cliente data, IFormFile file)
    {
        _clientesDbContext.Edit(data);
        return RedirectToAction("Index");
    }

    //[Authorize(Roles = "Admin")]
    public IActionResult Delete(Guid id)
    {
        _clientesDbContext.Delete(id);
        return RedirectToAction("Index");
    }


}