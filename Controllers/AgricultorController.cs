using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Plantech.Interfaces;
using Plantech.Services;

namespace Plantech.Controllers
{
    [Authorize(Roles = "Agricultor, Administrador")]
    public class AgricultorController(ILogger<AgricultorController> logger, IPlantioService plantioService) : Controller
    {
        private readonly ILogger<AgricultorController> _logger = logger;
        private readonly IPlantioService _plantioService = plantioService;

        public async Task<IActionResult> Index()
        {
            var plantios = await _plantioService.GetAllAsync();
            return View(plantios);
        }
    }
}
