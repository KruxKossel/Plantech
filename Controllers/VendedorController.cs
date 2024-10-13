using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Plantech.Controllers
{
    [Authorize(Roles = "Vendedor, Administrador")]
    public class VendedorController(ILogger<VendedorController> logger) : Controller
    {
        private readonly ILogger<VendedorController> _logger = logger;

        public IActionResult Index()
        {
            return View();
        }
    }
}