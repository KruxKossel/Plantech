using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plantech.Models;

namespace Plantech.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index()
    {
        return View();
    }

    // [Authorize(Roles = "Administrador")]
    // public IActionResult HomeAdministrador()
    // {
    //     return View();
    // }

    // [Authorize(Roles = "Agricultor")]
    // public IActionResult HomeAgricultor()
    // {
    //     return View();
    // }

    // [Authorize(Roles = "Comprador")]
    // public IActionResult HomeComprador()
    // {
    //     return View();
    // }

    // [Authorize(Roles = "Vendedor")]
    // public IActionResult HomeVendedor()
    // {
    //     return View();
    // }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
