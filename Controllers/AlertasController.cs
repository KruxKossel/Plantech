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
    public class AlertasController : Controller
    {
        private readonly PlantechContext _context;

        public AlertasController(PlantechContext context)
        {
            _context = context;
        }

        // GET: Alertas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Alertas.ToListAsync());
        }

        // GET: Alertas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alerta = await _context.Alertas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alerta == null)
            {
                return NotFound();
            }

            return View(alerta);
        }

        // GET: Alertas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alertas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LoteId,Tipo,Mensagem,DataCriacao")] Alerta alerta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alerta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alerta);
        }

        // GET: Alertas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alerta = await _context.Alertas.FindAsync(id);
            if (alerta == null)
            {
                return NotFound();
            }
            return View(alerta);
        }

        // POST: Alertas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LoteId,Tipo,Mensagem,DataCriacao")] Alerta alerta)
        {
            if (id != alerta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alerta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlertaExists(alerta.Id))
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
            return View(alerta);
        }

        // GET: Alertas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alerta = await _context.Alertas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alerta == null)
            {
                return NotFound();
            }

            return View(alerta);
        }

        // POST: Alertas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alerta = await _context.Alertas.FindAsync(id);
            if (alerta != null)
            {
                _context.Alertas.Remove(alerta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlertaExists(int id)
        {
            return _context.Alertas.Any(e => e.Id == id);
        }
    }
}
