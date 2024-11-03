using Plantech.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Plantech.Data;
using DocumentFormat.OpenXml.InkML;
using Plantech.ViewModels;
using AutoMapper;
using Plantech.DTOs;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Plantech.Controllers
{   [Authorize(Roles = "Comprador, Administrador")]
    public class OrdensComprasController : Controller
    {
        
        private readonly IUsuarioService _usuarioService;
        private readonly PlantechContext _context;
        private readonly IOrdensCompraService _ordensCompraService;
        private readonly IMapper _mapper;


        public OrdensComprasController(PlantechContext context, IOrdensCompraService ordensCompraService, IMapper mapper,IUsuarioService usuarioService)
        {
            _ordensCompraService = ordensCompraService;
            _context = context;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        [Authorize(Roles = "Comprador, Administrador")] 
        public async Task<IActionResult> Index()
        {
            var ordensCompra = await _ordensCompraService.ListarCompras();
            var ordensCompraDTO = _mapper.Map<List<OrdensCompraDTO>>(ordensCompra);
            return View(ordensCompraDTO);
        }
        [Authorize(Roles = "Comprador, Administrador")]
        [HttpGet]
        public async Task<IActionResult> NovaCompra()
        {
            var fornecedores = await _ordensCompraService.ListarFornecedoresAsync();; 
            var funcionarioid = User.FindFirst(ClaimTypes.Email)?.Value;
            ViewData["FornecedorId"] = new SelectList(fornecedores, "Id", "Cnpj");
            return View();
        }
        [Authorize(Roles = "Comprador, Administrador")]
        [HttpPost]
        public async Task<IActionResult> NovaCompra([Bind("FornecedorId")] OrdensCompraViewModel ordensCompraVM)
        {
            var ordensCompraDTO = _mapper.Map<OrdensCompraDTO>(ordensCompraVM);
            
            
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;

            var usuarioLogado = _usuarioService.GetByEmailAsync(useremail);
                // for(int i =0; i<100 ; i++)
                // Console.WriteLine("vazio"+ usuarioLogado.Id);

            var fornecedor = await _ordensCompraService.ObterFornecedorPorId(ordensCompraDTO.FornecedorId);
            if (fornecedor == null)
                {
                    
                    ModelState.AddModelError("FornecedorId", $"Fornecedor com ID {ordensCompraDTO.FornecedorId} não encontrado.");
                    return View(ordensCompraVM);
                }
                    
                // for(int i =0; i<100 ; i++)
                // Console.WriteLine($"DataCompra: {ordensCompraDTO.DataCompra}, FornecedorId: {ordensCompraDTO.FornecedorId}, FuncionarioId: {ordensCompraDTO.FuncionarioId}, Total: {ordensCompraDTO.Total}");
            if (fornecedor == null)
            {
                ModelState.AddModelError("FornecedorId", "Fornecedor não encontrado.");
                return View(ordensCompraVM);
            }

            ModelState.Remove("Status"); 
                ordensCompraDTO.Status = "pendente";
            {
            Debug.WriteLine(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
             if (!ModelState.IsValid)
            {
                Debug.WriteLine("Erros no ModelState:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Debug.WriteLine(error.ErrorMessage);
                }
            }
            if (ModelState.IsValid)
                 ordensCompraDTO.Total = 0;
                 ordensCompraDTO.DataCompra = DateOnly.FromDateTime(DateTime.Now);
                ordensCompraDTO.FuncionarioId= usuarioLogado.Id;
                await _ordensCompraService.CriarCompra(ordensCompraDTO);
                return RedirectToAction(nameof(Index)); 
            }
            return View(ordensCompraVM);
        }

        public async Task<IActionResult> LotesDisponiveis(){
            
            return View();
        }
        // GET: OrdensCompras/Details/5
        // public async Task<IActionResult> Details(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var ordensCompra = await _context.OrdensCompras
        //         .Include(o => o.Fornecedor)
        //         .Include(o => o.Funcionario)
        //         .FirstOrDefaultAsync(m => m.Id == id);
        //     if (ordensCompra == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(ordensCompra);
        // }

        // // GET: OrdensCompras/Create
        // public IActionResult Create()
        // {
        //     ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Id");
        //     ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Id");
        //     return View();
        // }

        // // POST: OrdensCompras/Create
        // // To protect from overposting attacks, enable the specific properties you want to bind to.
        // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("Id,Total,Status,FuncionarioId,FornecedorId,DataCompra")] OrdensCompra ordensCompra)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _context.Add(ordensCompra);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Id", ordensCompra.FornecedorId);
        //     ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Id", ordensCompra.FuncionarioId);
        //     return View(ordensCompra);
        // }

        // // GET: OrdensCompras/Edit/5
        // public async Task<IActionResult> Edit(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var ordensCompra = await _context.OrdensCompras.FindAsync(id);
        //     if (ordensCompra == null)
        //     {
        //         return NotFound();
        //     }
        //     ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Id", ordensCompra.FornecedorId);
        //     ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Id", ordensCompra.FuncionarioId);
        //     return View(ordensCompra);
        // }

        // // POST: OrdensCompras/Edit/5
        // // To protect from overposting attacks, enable the specific properties you want to bind to.
        // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, [Bind("Id,Total,Status,FuncionarioId,FornecedorId,DataCompra")] OrdensCompra ordensCompra)
        // {
        //     if (id != ordensCompra.Id)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(ordensCompra);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!OrdensCompraExists(ordensCompra.Id))
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
        //     ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Id", ordensCompra.FornecedorId);
        //     ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Id", ordensCompra.FuncionarioId);
        //     return View(ordensCompra);
        // }

        // // GET: OrdensCompras/Delete/5
        // public async Task<IActionResult> Delete(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var ordensCompra = await _context.OrdensCompras
        //         .Include(o => o.Fornecedor)
        //         .Include(o => o.Funcionario)
        //         .FirstOrDefaultAsync(m => m.Id == id);
        //     if (ordensCompra == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(ordensCompra);
        // }

        // // POST: OrdensCompras/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(int id)
        // {
        //     var ordensCompra = await _context.OrdensCompras.FindAsync(id);
        //     if (ordensCompra != null)
        //     {
        //         _context.OrdensCompras.Remove(ordensCompra);
        //     }

        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }

        // private bool OrdensCompraExists(int id)
        // {
        //     return _context.OrdensCompras.Any(e => e.Id == id);
        // }
    }
}
