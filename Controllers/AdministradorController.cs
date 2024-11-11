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
    [Authorize(Roles = "Administrador")]
    public class AdministradorController(ILogger<AdministradorController> logger) : Controller
    {
        private readonly ILogger<AdministradorController> _logger = logger;

        public IActionResult Index()
        {
            return View();
        }

        

    }
}