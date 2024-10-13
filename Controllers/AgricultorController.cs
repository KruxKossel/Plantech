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
[Authorize(Roles = "Agricultor, Administrador")]
    public class AgricultorController(ILogger<AgricultorController> logger) : Controller
    {
        private readonly ILogger<AgricultorController> _logger = logger;

        public IActionResult Index()
        {
            return View();
        }

    }
}