using AutoMapper;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plantech.Data;
using Plantech.DTOs;
using Plantech.Interfaces;
using Plantech.Models;

namespace Plantech.Controllers
{
    public class FornecedoresController : Controller
    {
        private readonly IFornecedorService _fornecedorService;

        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorService fornecedoresService, IMapper mapper)
        {
            _fornecedorService = fornecedoresService;
            _mapper=mapper;
        }

        // GET: Fornecedores
        public async Task<IActionResult> Index()
        {
            return View(await _fornecedorService.ListarAsync());
        }

        // GET: Fornecedores/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
            return NotFound();
            }
            var fornecedore = await _fornecedorService.ObterPorIdAsync(id);
            if (fornecedore == null)
            {
                return NotFound();
            }
            var fornecedoreDTO = _mapper.Map<FornecedoreDTO>(fornecedore);
            return View(fornecedoreDTO);
        }

        // GET: Fornecedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fornecedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cnpj,RazaoSocial,Cidade,Endereco,Email,Status")] FornecedoreViewModel fornecedore)
        {
            var fornecedoreDTO = _mapper.Map<FornecedoreDTO>(fornecedore);
            if (ModelState.IsValid)
            {
                _fornecedorService.AdicionarAsync(fornecedoreDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(fornecedore);
        }

        // GET: Fornecedores/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedorDTO = await _fornecedorService.ObterPorIdAsync(id);
            if (fornecedorDTO == null)
            {
                return NotFound();
            }
            return View(fornecedorDTO);
        }

        // POST: Fornecedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cnpj,RazaoSocial,Cidade,Endereco,Email,Status")] FornecedoreViewModel fornecedoreVM)
        {
            if (id != fornecedoreVM.Id)
            {
                return NotFound();
            }
            var fornecedoreDTO = _mapper.Map<FornecedoreDTO>(fornecedoreVM);
            if (ModelState.IsValid)
            {
                try
                {
                    await _fornecedorService.AtualizarDadosAsync(fornecedoreDTO);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FornecedoreExists(fornecedoreDTO.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fornecedoreVM);
        }

    
        // POST: Fornecedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var fornecedore = _fornecedorService.AtualizarStatusAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool FornecedoreExists(int id)
        {
            bool retorno;
            var fornecedor = _fornecedorService.ObterPorIdAsync(id);
            if(fornecedor == null){
                retorno = false;
            }else{
                retorno = true;
            }
            
            return retorno;
        }
    }
}
