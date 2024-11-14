using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plantech.Interfaces;
using Plantech.ViewModels;

namespace Plantech.Controllers
{
    [Authorize(Roles = "Comprador, Administrador, Agricultor")]
    public class LotesInsumosController(ILotesInsumosService lotesInsumosService, IInsumoService insumoService, IMapper mapper) : Controller
    {
        private readonly ILotesInsumosService _lotesInsumosService = lotesInsumosService;
        private readonly IInsumoService _insumoService = insumoService;
        private readonly IMapper _mapper = mapper;


        [Authorize(Roles = "Comprador, Administrador, Agricultor")]
        // GET: LotesHortalicas
        public async Task<IActionResult> Index()
        {
            return View(await _lotesInsumosService.ListarLotes());
        }


        [Authorize(Roles = "Comprador, Administrador, Agricultor")]
        // GET: LotesHortalicas/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            
            var lotesInsumo = await _lotesInsumosService.GetLotesInsumoId(id);
            if (lotesInsumo == null)
            {
                return NotFound();
            }
            return View(lotesInsumo);
        }

        [Authorize(Roles = "Administrador")]
        // GET: LotesHortalicas/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lotesInsumo = await _lotesInsumosService.GetLotesInsumoId(id);
            if (lotesInsumo  == null)
            {
                return NotFound();
            }

            var lotesInsumoVM =  _mapper.Map<LotesInsumoViewModel>(lotesInsumo);
            return View(lotesInsumoVM);
        }
        
        
        
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, DataValidade, Status, Nome")] LotesInsumoViewModel lotesInsumo)
        {
            if (id != lotesInsumo.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Key: {state.Key}, Error: {error.ErrorMessage}");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Carrega o registro existente do banco de dados
                    var loteExistente = await _lotesInsumosService.GetLotesInsumoId(id);
                    if (loteExistente == null)
                    {
                        return NotFound();
                    }

                    // Atualiza apenas as propriedades necessárias, mantendo o Status
                    loteExistente.DataValidade = lotesInsumo.DataValidade;
                    loteExistente.Nome = lotesInsumo.Nome;
                    // loteExistente.Status = 

                    // Atualiza no banco de dados sem sobrescrever o Status
                    await _lotesInsumosService.EditarLote(loteExistente);

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LotesHortalicaExists(lotesInsumo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(lotesInsumo);
        }

        private bool LotesHortalicaExists(int id)
        {
            bool retorno;
            var fornecedor = _lotesInsumosService.GetLotesInsumoId(id);
            if(fornecedor == null){
                retorno = false;
            }else{
                retorno = true;
            }
            
            return retorno;
        }
    }
    }

