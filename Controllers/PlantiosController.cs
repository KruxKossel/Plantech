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
    public class PlantiosController(PlantechContext context) : Controller
    {
        private readonly PlantechContext _context = context;

        // GET: Plantios

        public async Task<IActionResult> Index()
        {
            var plantechContext = _context.Plantios.Include(p => p.Funcionario).Include(p => p.Hortalica);
            return View(await plantechContext.ToListAsync());
        }

        // GET: Plantios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantio = await _context.Plantios
                .Include(p => p.Funcionario)
                .Include(p => p.Hortalica)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plantio == null)
            {
                return NotFound();
            }

            return View(plantio);
        }

        // GET: Plantios/Create
        public IActionResult Create()
        {
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Id");
            ViewData["HortalicaId"] = new SelectList(_context.Hortalicas, "Id", "Id");
            return View();
        }

        // POST: Plantios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataPlantio,HortalicaId,FuncionarioId,Quantidade")] Plantio plantio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plantio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Id", plantio.FuncionarioId);
            ViewData["HortalicaId"] = new SelectList(_context.Hortalicas, "Id", "Id", plantio.HortalicaId);
            return View(plantio);
        }

        // GET: Plantios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantio = await _context.Plantios.FindAsync(id);
            if (plantio == null)
            {
                return NotFound();
            }
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Id", plantio.FuncionarioId);
            ViewData["HortalicaId"] = new SelectList(_context.Hortalicas, "Id", "Id", plantio.HortalicaId);
            return View(plantio);
        }

        // POST: Plantios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataPlantio,HortalicaId,FuncionarioId,Quantidade")] Plantio plantio)
        {
            if (id != plantio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plantio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantioExists(plantio.Id))
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
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Id", plantio.FuncionarioId);
            ViewData["HortalicaId"] = new SelectList(_context.Hortalicas, "Id", "Id", plantio.HortalicaId);
            return View(plantio);
        }

        // GET: Plantios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantio = await _context.Plantios
                .Include(p => p.Funcionario)
                .Include(p => p.Hortalica)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plantio == null)
            {
                return NotFound();
            }

            return View(plantio);
        }

        // POST: Plantios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plantio = await _context.Plantios.FindAsync(id);
            if (plantio != null)
            {
                _context.Plantios.Remove(plantio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantioExists(int id)
        {
            return _context.Plantios.Any(e => e.Id == id);
        }
    }
}
