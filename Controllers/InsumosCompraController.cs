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
    public class InsumosCompraController : Controller
    {
        private readonly PlantechContext _context;

        public InsumosCompraController(PlantechContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {

             var plantechContext = _context.InsumosCompras
                .Include(h => h.Lote)
                .Include(h => h.OrdemCompra);
        
            return View(await plantechContext.ToListAsync());
        }

        
        // public async Task<IActionResult> Details(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var insumosCompra = await _context.InsumosCompras
        //         .Include(h => h.Lote)
        //         .Include(h => h.OrdemCompra)
        //         .FirstOrDefaultAsync(m => m.OrdemCompraId == id);
        //     if (insumosCompra == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(insumosCompra);
        // }

        // public IActionResult Create()
        // {
        //     ViewData["LoteId"] = new SelectList(_context.LotesInsumos, "Id", "Id");
        //     ViewData["OrdemCompraId"] = new SelectList(_context.OrdensCompras, "Id", "Id");
        //     return View();
        // }


        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("VendaId,LoteId,Quantidade,PrecoUnitario")] InsumosCompra insumosCompras)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _context.Add(insumosCompras);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     ViewData["LoteId"] = new SelectList(_context.LotesInsumos, "Id", "Id", insumosCompras.LoteId);
        //     ViewData["OrdemCompraId"] = new SelectList(_context.OrdensCompras, "Id", "Id", insumosCompras.OrdemCompraId);
        //     return View(insumosCompras);
        // }

        // public async Task<IActionResult> Edit(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var insumosCompra = await _context.InsumosCompras.FindAsync(id);
        //     if (insumosCompra == null)
        //     {
        //         return NotFound();
        //     }
        //     ViewData["LoteId"] = new SelectList(_context.LotesInsumos, "Id", "Id", insumosCompra.LoteId);
        //     ViewData["OrdemCompraId"] = new SelectList(_context.OrdensCompras, "Id", "Id", insumosCompra.OrdemCompraId);
        //     return View(insumosCompra);
        // }

        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, [Bind("VendaId,LoteId,Quantidade,PrecoUnitario")] InsumosCompra insumosCompra)
        // {
        //     if (id != insumosCompra.OrdemCompraId)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(insumosCompra);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!InsumosCompraExists(insumosCompra.OrdemCompraId))
        //             {
        //                 return NotFound();
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     ViewData["LoteId"] = new SelectList(_context.LotesInsumos, "Id", "Id", insumosCompra.LoteId);
        //     ViewData["OrdemCompraId"] = new SelectList(_context.OrdensCompras, "Id", "Id", insumosCompra.OrdemCompraId);
        //     return View(insumosCompra);
        // }


        // public async Task<IActionResult> Delete(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var insumosCompra = await _context.InsumosCompras
        //         .Include(h => h.Lote)
        //         .Include(h => h.OrdemCompra)
        //         .FirstOrDefaultAsync(m => m.OrdemCompraId == id);
        //     if (insumosCompra == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(insumosCompra);
        // }

        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(int id)
        // {
        //     var insumosCompra = await _context.InsumosCompras.FindAsync(id);
        //     if (insumosCompra != null)
        //     {
        //         _context.InsumosCompras.Remove(insumosCompra);
        //     }

        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }

        // private bool InsumosCompraExists(int id)
        // {
        //     return _context.InsumosCompras.Any(e => e.OrdemCompraId == id);
        // }
    }
}
