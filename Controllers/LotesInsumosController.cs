using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Plantech.Data;
using Plantech.Models;

namespace Plantech.Controllers
{
    
    public class LotesInsumosController : Controller
    {
        private readonly PlantechContext _context;

        public LotesInsumosController(PlantechContext context)
        {
            _context = context;
        }

        // GET: LotesHortalicas
        public async Task<IActionResult> Index()
        {
            var plantechContext = _context.LotesInsumos.Include(l => l.Insumo);
            return View(await plantechContext.ToListAsync());
        }

        // GET: LotesHortalicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lotesInsumo = await _context.LotesInsumos
                .Include(l => l.Insumo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lotesInsumo == null)
            {
                return NotFound();
            }

            return View(lotesInsumo);
        }


        // GET: LotesHortalicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lotesInsumo = await _context.LotesInsumos.FindAsync(id);
            if (lotesInsumo  == null)
            {
                return NotFound();
            }
            ViewData["HortalicaId"] = new SelectList(_context.Insumos, "Id", "Id", lotesInsumo.InsumoId);
            return View(lotesInsumo);
        }

        // POST: LotesHortalicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, PrecoVenda, DataValidade, Status, Nome")] LotesInsumo lotesInsumo)
        {
            if (id != lotesInsumo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lotesInsumo);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["HortalicaId"] = new SelectList(_context.Insumos, "Id", "Id", lotesInsumo.InsumoId);
            return View(lotesInsumo);
        }

        private bool LotesHortalicaExists(int id)
        {
            return _context.LotesInsumos.Any(e => e.Id == id);
        }
    }
}
