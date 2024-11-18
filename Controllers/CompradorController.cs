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
    [Authorize(Roles = "Comprador, Administrador")]
    public class CompradorController(ILogger<CompradorController> logger, ILotesInsumosService lotesInsumosService) : Controller
    {
        private readonly ILogger<CompradorController> _logger = logger;

        private readonly ILotesInsumosService _lotesInsumosService = lotesInsumosService;

        public async Task<IActionResult> Index()
        {
            var lotes = await _lotesInsumosService.ListarLotes();
            var lotesAgrupados = lotes
                .GroupBy(l => l.Nome)
                .Select(g => new Plantech.DTOs.LotesInsumoDTO 
                { 
                    Nome = g.Key, 
                    Quantidade = g.Sum(l => l.Quantidade) 
                }).ToList();
            return View(lotesAgrupados);
        }


    }
}