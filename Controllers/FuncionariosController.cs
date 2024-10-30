using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plantech.ViewModels;

namespace Plantech.Controllers;


 [Authorize(Roles = "Administrador")]
public class FuncionariosController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<ActionResult> Create(){

        
        var model = new FuncionarioViewModel();

        model.NomeUsuario = "Deltrano";
        
        ViewData["NomeUsuario"] =  model.NomeUsuario;

        return View(model);
    }
}
