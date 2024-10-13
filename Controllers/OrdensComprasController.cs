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
    public class OrdensComprasController : Controller
    {
        private readonly PlantechContext _context;

        public OrdensComprasController(PlantechContext context)
        {
            _context = context;
        }

        // GET: OrdensCompras
        public async Task<IActionResult> Index()
        {
            var plantechContext = _context.OrdensCompras.Include(o => o.Fornecedor).Include(o => o.Funcionario);
            return View(await plantechContext.ToListAsync());
        }

        // GET: OrdensCompras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordensCompra = await _context.OrdensCompras
                .Include(o => o.Fornecedor)
                .Include(o => o.Funcionario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordensCompra == null)
            {
                return NotFound();
            }

            return View(ordensCompra);
        }

        // GET: OrdensCompras/Create
        public IActionResult Create()
        {
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Id");
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Id");
            return View();
        }

        // POST: OrdensCompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Total,Status,FuncionarioId,FornecedorId,DataCompra")] OrdensCompra ordensCompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordensCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Id", ordensCompra.FornecedorId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Id", ordensCompra.FuncionarioId);
            return View(ordensCompra);
        }

        // GET: OrdensCompras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordensCompra = await _context.OrdensCompras.FindAsync(id);
            if (ordensCompra == null)
            {
                return NotFound();
            }
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Id", ordensCompra.FornecedorId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Id", ordensCompra.FuncionarioId);
            return View(ordensCompra);
        }

        // POST: OrdensCompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Total,Status,FuncionarioId,FornecedorId,DataCompra")] OrdensCompra ordensCompra)
        {
            if (id != ordensCompra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordensCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdensCompraExists(ordensCompra.Id))
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
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Id", ordensCompra.FornecedorId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Id", ordensCompra.FuncionarioId);
            return View(ordensCompra);
        }

        // GET: OrdensCompras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordensCompra = await _context.OrdensCompras
                .Include(o => o.Fornecedor)
                .Include(o => o.Funcionario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordensCompra == null)
            {
                return NotFound();
            }

            return View(ordensCompra);
        }

        // POST: OrdensCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ordensCompra = await _context.OrdensCompras.FindAsync(id);
            if (ordensCompra != null)
            {
                _context.OrdensCompras.Remove(ordensCompra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdensCompraExists(int id)
        {
            return _context.OrdensCompras.Any(e => e.Id == id);
        }
    }
}
