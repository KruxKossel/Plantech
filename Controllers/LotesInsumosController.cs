using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Plantech.DTOs;
using Plantech.Interfaces;
using Plantech.ViewModels;

namespace Plantech.Controllers
{
    
    public class LotesInsumosController : Controller
    {
        private readonly ILotesInsumosService _lotesInsumosService;
        private readonly IInsumoService _insumoService;
        private readonly IMapper _mapper;

        public LotesInsumosController(ILotesInsumosService lotesInsumosService,IInsumoService insumoService, IMapper mapper)
        {
            _lotesInsumosService = lotesInsumosService;
            _insumoService = insumoService;
            _mapper = mapper;
        }

        // GET: LotesHortalicas
        public async Task<IActionResult> Index()
        {
            return View(await _lotesInsumosService.ListarLotes());
        }

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

        // POST: LotesHortalicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
   public async Task<IActionResult> Edit(int id, [Bind("Id, DataValidade, Nome")] LotesInsumoViewModel lotesInsumo)
{
    if (id != lotesInsumo.Id)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            var lotesinsumoDTO = _mapper.Map<LotesInsumoDTO>(lotesInsumo);
            await _lotesInsumosService.EditarLote(lotesinsumoDTO);
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
