using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Plantech.Data;
using Plantech.Interfaces;
using Plantech.Models;
using Plantech.ViewModels;

namespace Plantech.Controllers
{
    [Authorize(Roles = "Agricultor, Administrador, Vendedor")]
    public class LotesHortalicasController(ILotesHortalicasService lotesHortalicasService, IMapper mapper) : Controller
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILotesHortalicasService _lotesHortalicasSerivce = lotesHortalicasService;

        // GET: LotesHortalicas
        public async Task<IActionResult> Index()
        {
            var lotesDTO = await _lotesHortalicasSerivce.ListarLotes();
            var lotesVM = _mapper.Map<List<LotesHortalicaViewModel>>(lotesDTO);
            return View(lotesVM);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Pendencias()
        {
            var lotesDTO = await _lotesHortalicasSerivce.ListarLotes();
            var lotesVM = _mapper.Map<List<LotesHortalicaViewModel>>(lotesDTO);
            return View(lotesVM);
        }

        // GET: LotesHortalicas/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loteDTO = await _lotesHortalicasSerivce.GetLotesInsumoId(id);
            var loteVM = _mapper.Map<LotesHortalicaViewModel>(loteDTO);
            if (loteVM == null)
            {
                return NotFound();
            }

            return View(loteVM);
        }


        // GET: LotesHortalicas/Edit/5
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loteDTO = await _lotesHortalicasSerivce.GetLotesInsumoId(id);
            if (loteDTO  == null)
            {
                return NotFound();
            }

            var loteVM =  _mapper.Map<LotesHortalicaPendenciaViewModel>(loteDTO);
            return View(loteVM);
        }

        // POST: LotesHortalicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("Id, PrecoVenda, DataValidade, Status, Nome")] 
                                                LotesHortalicaPendenciaViewModel lotesvm)
        {
            if (id != lotesvm.Id)
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
                    var loteExistente = await _lotesHortalicasSerivce.GetLotesInsumoId(id);
                    if (loteExistente == null)
                    {
                        return NotFound();
                    }

                    // Atualiza apenas as propriedades necessárias, mantendo o Status
                    loteExistente.PrecoVenda = lotesvm.PrecoVenda;
                    loteExistente.DataValidade = lotesvm.DataValidade;
                    loteExistente.Nome = lotesvm.Nome;
                    // loteExistente.Status = 

                    // Atualiza no banco de dados sem sobrescrever o Status
                    await _lotesHortalicasSerivce.EditarLote(loteExistente);

                    return RedirectToAction(nameof(Pendencias));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LotesHortalicaExists(lotesvm.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(lotesvm);
        }

        private bool LotesHortalicaExists(int id)
        {
                        bool retorno;
            var lote = _lotesHortalicasSerivce.GetLotesInsumoId(id);
            if(lote == null){
                retorno = false;
            }else{
                retorno = true;
            }
            
            return retorno;
        }
    }
}
