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
                                            ILotesInsumosService lotesInsumosService, IRelatorioService relatorioService) : Controller
    {
        private readonly ILogger<AdministradorController> _logger = logger;
        private readonly ILotesHortalicasService _lotesHortalicasService = lotesHortalicasService;
        private readonly ILotesInsumosService _lotesInsumosService = lotesInsumosService;
        private readonly IRelatorioService _relatorioService = relatorioService;

        public async  Task<IActionResult> Index()
        {
            var relatorioViewModel = await _relatorioService.ProcessandoDados(); 
            return View(relatorioViewModel); 
        }

        [HttpGet]
        public async Task<IActionResult> Relatorio()
        {
            return View(await _relatorioService.ProcessandoDados());
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