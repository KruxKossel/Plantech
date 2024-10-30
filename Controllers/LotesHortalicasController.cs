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
    [Authorize(Roles = "Agricultor, Administrador, Vendedor")]
    public class LotesHortalicasController(PlantechContext context) : Controller
    {
        private readonly PlantechContext _context = context;

        // GET: LotesHortalicas

        public async Task<IActionResult> Index()
        {
            var plantechContext = _context.LotesHortalicas.Include(l => l.Hortalica);
            return View(await plantechContext.ToListAsync());
        }

        // GET: LotesHortalicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lotesHortalica = await _context.LotesHortalicas
                .Include(l => l.Hortalica)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lotesHortalica == null)
            {
                return NotFound();
            }

            return View(lotesHortalica);
        }


        // GET: LotesHortalicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lotesHortalica = await _context.LotesHortalicas.FindAsync(id);
            if (lotesHortalica == null)
            {
                return NotFound();
            }
            ViewData["HortalicaId"] = new SelectList(_context.Hortalicas, "Id", "Id", lotesHortalica.HortalicaId);
            return View(lotesHortalica);
        }

        // POST: LotesHortalicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, PrecoVenda, DataValidade, Status, Nome")] LotesHortalica lotesHortalica)
        {
            if (id != lotesHortalica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lotesHortalica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LotesHortalicaExists(lotesHortalica.Id))
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
            ViewData["HortalicaId"] = new SelectList(_context.Hortalicas, "Id", "Id", lotesHortalica.HortalicaId);
            return View(lotesHortalica);
        }

        private bool LotesHortalicaExists(int id)
        {
            return _context.LotesHortalicas.Any(e => e.Id == id);
        }
    }
}
