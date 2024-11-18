using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Plantech.DTOs;
using Plantech.Interfaces;

namespace Plantech.Controllers
{
    [Authorize(Roles = "Vendedor, Administrador")]
    public class VendedorController(ILogger<VendedorController> logger, IVendasService vendasService) : Controller
    {
        private readonly ILogger<VendedorController> _logger = logger;

        private readonly IVendasService _vendasService = vendasService;

        public async Task<IActionResult> Index()
        {
            var vendas = await _vendasService.ListarVendas();
            var vendasAgrupadas = vendas
                .GroupBy(v => v.Data)
                .Select(g => new VendaDTO 
                { 
                    Data = g.Key, 
                    TotalVendas = g.Sum(v => v.TotalVendas) 
                }).ToList();
            return View(vendasAgrupadas);
        }



    }
}