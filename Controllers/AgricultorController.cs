using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Plantech.Interfaces;
using Plantech.Services;
using Plantech.ViewModels;

namespace Plantech.Controllers
{
    [Authorize(Roles = "Agricultor, Administrador")]
    public class AgricultorController(ILogger<AgricultorController> logger, IPlantioService plantioService, IColheitaService colheitaService) : Controller
    {
        private readonly ILogger<AgricultorController> _logger = logger;
        private readonly IPlantioService _plantioService = plantioService;

        private readonly IColheitaService _colheitaService = colheitaService;

        public async Task<IActionResult> Index()
        {
            var plantios = await _plantioService.GetAllAsync();
            var colheitas = await _colheitaService.GetAllAsync();

            var viewModel = new PlantioColheitaViewModel
            {
                Plantios = plantios,
                Colheitas = colheitas
            };

            return View(viewModel);
        }

        public async Task<IActionResult> CulturasPerdidas()
        {

            var perdas = new CulturasPerdidasViewModel();

            

            return View(perdas);
        }

    }
}
