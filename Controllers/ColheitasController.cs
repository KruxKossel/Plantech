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
    public class ColheitasController : Controller
    {
        private readonly PlantechContext _context;

        public ColheitasController(PlantechContext context)
        {
            _context = context;
        }

        // GET: Colheitas
        public async Task<IActionResult> Index()
        {
            var plantechContext = _context.Colheitas.Include(c => c.Funcionario).Include(c => c.LoteHortalica).Include(c => c.LoteInsumo).Include(c => c.Plantio);
            return View(await plantechContext.ToListAsync());
        }

        // GET: Colheitas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colheita = await _context.Colheitas
                .Include(c => c.Funcionario)
                .Include(c => c.LoteHortalica)
                .Include(c => c.LoteInsumo)
                .Include(c => c.Plantio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (colheita == null)
            {
                return NotFound();
            }

            return View(colheita);
        }

        // GET: Colheitas/Create
        public IActionResult Create()
        {
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Id");
            ViewData["LoteHortalicaId"] = new SelectList(_context.LotesHortalicas, "Id", "Id");
            ViewData["LoteInsumoId"] = new SelectList(_context.LotesInsumos, "Id", "Id");
            ViewData["PlantioId"] = new SelectList(_context.Plantios, "Id", "Id");
            return View();
        }

        // POST: Colheitas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantidade,DataColheita,FuncionarioId,LoteHortalicaId,LoteInsumoId,PlantioId")] Colheita colheita)
        {
            if (ModelState.IsValid)
            {
                _context.Add(colheita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Id", colheita.FuncionarioId);
            ViewData["LoteHortalicaId"] = new SelectList(_context.LotesHortalicas, "Id", "Id", colheita.LoteHortalicaId);
            ViewData["LoteInsumoId"] = new SelectList(_context.LotesInsumos, "Id", "Id", colheita.LoteInsumoId);
            ViewData["PlantioId"] = new SelectList(_context.Plantios, "Id", "Id", colheita.PlantioId);
            return View(colheita);
        }

        // GET: Colheitas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colheita = await _context.Colheitas.FindAsync(id);
            if (colheita == null)
            {
                return NotFound();
            }
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Id", colheita.FuncionarioId);
            ViewData["LoteHortalicaId"] = new SelectList(_context.LotesHortalicas, "Id", "Id", colheita.LoteHortalicaId);
            ViewData["LoteInsumoId"] = new SelectList(_context.LotesInsumos, "Id", "Id", colheita.LoteInsumoId);
            ViewData["PlantioId"] = new SelectList(_context.Plantios, "Id", "Id", colheita.PlantioId);
            return View(colheita);
        }

        // POST: Colheitas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantidade,DataColheita,FuncionarioId,LoteHortalicaId,LoteInsumoId,PlantioId")] Colheita colheita)
        {
            if (id != colheita.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(colheita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColheitaExists(colheita.Id))
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
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Id", colheita.FuncionarioId);
            ViewData["LoteHortalicaId"] = new SelectList(_context.LotesHortalicas, "Id", "Id", colheita.LoteHortalicaId);
            ViewData["LoteInsumoId"] = new SelectList(_context.LotesInsumos, "Id", "Id", colheita.LoteInsumoId);
            ViewData["PlantioId"] = new SelectList(_context.Plantios, "Id", "Id", colheita.PlantioId);
            return View(colheita);
        }

        // GET: Colheitas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colheita = await _context.Colheitas
                .Include(c => c.Funcionario)
                .Include(c => c.LoteHortalica)
                .Include(c => c.LoteInsumo)
                .Include(c => c.Plantio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (colheita == null)
            {
                return NotFound();
            }

            return View(colheita);
        }

        // POST: Colheitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var colheita = await _context.Colheitas.FindAsync(id);
            if (colheita != null)
            {
                _context.Colheitas.Remove(colheita);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColheitaExists(int id)
        {
            return _context.Colheitas.Any(e => e.Id == id);
        }
    }
}
