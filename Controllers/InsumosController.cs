using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Plantech.Data;
using Plantech.Models;

namespace Plantech.Controllers
{
    public class InsumosController : Controller
    {
        private readonly PlantechContext _context;

        public InsumosController(PlantechContext context)
        {
            _context = context;
        }

        // GET: Insumos
        public async Task<IActionResult> Index()
        {
            var plantechContext = _context.Insumos.Include(i => i.Fornecedor);
            return View(await plantechContext.ToListAsync());
        }

        // GET: Insumos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insumo = await _context.Insumos
                .Include(i => i.Fornecedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insumo == null)
            {
                return NotFound();
            }

            return View(insumo);
        }

        // GET: Insumos/Create
        public IActionResult Create()
        {
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Id");
            return View();
        }

        // POST: Insumos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FornecedorId,Nome,Descricao,Observacoes,Categoria,Tipo,CaminhoImagem")] Insumo insumo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insumo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Id", insumo.FornecedorId);
            return View(insumo);
        }

        // GET: Insumos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insumo = await _context.Insumos.FindAsync(id);
            if (insumo == null)
            {
                return NotFound();
            }
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Id", insumo.FornecedorId);
            return View(insumo);
        }

        // POST: Insumos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FornecedorId,Nome,Descricao,Observacoes,Categoria,Tipo,CaminhoImagem")] Insumo insumo)
        {
            if (id != insumo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insumo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsumoExists(insumo.Id))
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
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Id", insumo.FornecedorId);
            return View(insumo);
        }

        // GET: Insumos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insumo = await _context.Insumos
                .Include(i => i.Fornecedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insumo == null)
            {
                return NotFound();
            }

            return View(insumo);
        }

        // POST: Insumos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insumo = await _context.Insumos.FindAsync(id);
            if (insumo != null)
            {
                _context.Insumos.Remove(insumo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsumoExists(int id)
        {
            return _context.Insumos.Any(e => e.Id == id);
        }
    }
}
