using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Plantech.Interfaces;

namespace Plantech.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdministradorController(ILogger<AdministradorController> logger, ILotesHortalicasService lotesHortalicasService, 
                                            ILotesInsumosService lotesInsumosService) : Controller
    {
        private readonly ILogger<AdministradorController> _logger = logger;
        private readonly ILotesHortalicasService _lotesHortalicasService = lotesHortalicasService;
        private readonly ILotesInsumosService _lotesInsumosService = lotesInsumosService;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Pendencias()
        {
            // chamar lotes de hortaliça, selecionar todas que não possuem preço
            // exibir com view model a quantidade

            // Chamar lotes de insumos, selecionar todos que possuem Rejeito no nome
            // exibir com a viem model a quantidade

            // fazer um count e por na quantidade
            return View();
        }


        

    }
}