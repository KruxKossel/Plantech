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
    public class HortalicasVendasController : Controller
    {
        private readonly PlantechContext _context;

        public HortalicasVendasController(PlantechContext context)
        {
            _context = context;
        }

        // GET: HortalicasVendas
        public async Task<IActionResult> Index()
        {
            var plantechContext = _context.HortalicasVendas.Include(h => h.Lote).Include(h => h.Venda);
            return View(await plantechContext.ToListAsync());
        }

        // GET: HortalicasVendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hortalicasVenda = await _context.HortalicasVendas
                .Include(h => h.Lote)
                .Include(h => h.Venda)
                .FirstOrDefaultAsync(m => m.VendaId == id);
            if (hortalicasVenda == null)
            {
                return NotFound();
            }

            return View(hortalicasVenda);
        }

        // GET: HortalicasVendas/Create
        public IActionResult Create()
        {
            ViewData["LoteId"] = new SelectList(_context.LotesHortalicas, "Id", "Id");
            ViewData["VendaId"] = new SelectList(_context.Vendas, "Id", "Id");
            return View();
        }

        // POST: HortalicasVendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendaId,LoteId,Quantidade,PrecoUnitario")] HortalicasVendas hortalicasVenda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hortalicasVenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoteId"] = new SelectList(_context.LotesHortalicas, "Id", "Id", hortalicasVenda.LoteId);
            ViewData["VendaId"] = new SelectList(_context.Vendas, "Id", "Id", hortalicasVenda.VendaId);
            return View(hortalicasVenda);
        }

        // GET: HortalicasVendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hortalicasVenda = await _context.HortalicasVendas.FindAsync(id);
            if (hortalicasVenda == null)
            {
                return NotFound();
            }
            ViewData["LoteId"] = new SelectList(_context.LotesHortalicas, "Id", "Id", hortalicasVenda.LoteId);
            ViewData["VendaId"] = new SelectList(_context.Vendas, "Id", "Id", hortalicasVenda.VendaId);
            return View(hortalicasVenda);
        }

        // POST: HortalicasVendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VendaId,LoteId,Quantidade,PrecoUnitario")] HortalicasVendas hortalicasVenda)
        {
            if (id != hortalicasVenda.VendaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hortalicasVenda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HortalicasVendaExists(hortalicasVenda.VendaId))
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
            ViewData["LoteId"] = new SelectList(_context.LotesHortalicas, "Id", "Id", hortalicasVenda.LoteId);
            ViewData["VendaId"] = new SelectList(_context.Vendas, "Id", "Id", hortalicasVenda.VendaId);
            return View(hortalicasVenda);
        }

        // GET: HortalicasVendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hortalicasVenda = await _context.HortalicasVendas
                .Include(h => h.Lote)
                .Include(h => h.Venda)
                .FirstOrDefaultAsync(m => m.VendaId == id);
            if (hortalicasVenda == null)
            {
                return NotFound();
            }

            return View(hortalicasVenda);
        }

        // POST: HortalicasVendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hortalicasVenda = await _context.HortalicasVendas.FindAsync(id);
            if (hortalicasVenda != null)
            {
                _context.HortalicasVendas.Remove(hortalicasVenda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HortalicasVendaExists(int id)
        {
            return _context.HortalicasVendas.Any(e => e.VendaId == id);
        }
    }
}
