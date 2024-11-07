using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Plantech.Data;
using Plantech.DTOs;
using Plantech.Interfaces;
using Plantech.Models;
using Plantech.ViewModels;

namespace Plantech.Controllers
{
    public class VendasController : Controller
    {
        // private readonly PlantechContext _context;
        private readonly IVendasService _vendasService;
        private readonly IMapper _mapper;
        private readonly IClienteService _clienteService;
        private readonly IUsuarioService _usuarioService;
        private async Task<int> GetLoggedFuncionarioId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                Console.WriteLine("pqp");
                throw new InvalidOperationException("User is not authenticated.");
            }

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new FormatException($"O valor '{userIdClaim.Value}' não é um número válido.");
            }

            var usuario = await _usuarioService.GetByIdAsync(userId);
            var funcionario = usuario.Funcionarios.FirstOrDefault();
            return funcionario != null ? funcionario.Id : 0;
        }

        public VendasController(IVendasService vendasServvice, IMapper mapper,IClienteService clienteService,IUsuarioService usuarioService)
        {
            // _context = context;
            _vendasService = vendasServvice;
            _mapper = mapper;
            _clienteService = clienteService;
            _usuarioService = usuarioService;
        }

        // GET: Vendas
        public async Task<IActionResult> Index()
        {
            var vendas = await _vendasService.ListarVendas();
            var vendasvm =_mapper.Map<List<VendaViewModel>>(vendas);
            return View(vendasvm);
        }

        [HttpGet]
        public async Task<IActionResult> NovaVenda()
        {
            var clientes = await _clienteService.ListarAsync();
            var clientesVm = _mapper.Map<IEnumerable<ClienteViewModel>>(clientes);
            ViewData["ClienteId"] = new SelectList(clientesVm, "Id", "Cnpj");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NovaVenda([Bind("ClienteId")] VendaViewModel vendavm)
        {
            var usuarioLogadoID =  await GetLoggedFuncionarioId();
            var usuariobj = _usuarioService.GetByIdAsync(usuarioLogadoID);
            Console.WriteLine("\n\n\n Entrou no Post \n\n\n");
            var vendadto = _mapper.Map<VendaDTO>(vendavm);
            vendadto.FuncionarioId = usuarioLogadoID;
            
            if (!ModelState.IsValid)
            {
                Console.WriteLine("\n\n\n ModelState Errors: \n\n\n");
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            
            if (ModelState.IsValid)
            {
                Console.WriteLine($"\n\n\n Entrou no ModelState\n Id Cliente {vendavm.ClienteId},\n Status {vendavm.Status}\n Data {vendavm.Data} \n   \n\n\n");
                var idvenda = await _vendasService.CriarVenda(vendadto);
                return RedirectToAction(nameof(Index), new { id = idvenda });
            }
            var clientes = await _clienteService.ListarAsync();
            var clientesVm = _mapper.Map<IEnumerable<ClienteViewModel>>(clientes);
            ViewData["ClienteId"] = new SelectList(clientesVm, "Id", "Cnpj");
            return View(vendavm);
        }

        // // GET: Vendas/Edit/5
        // public async Task<IActionResult> Edit(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var venda = await _context.Vendas.FindAsync(id);
        //     if (venda == null)
        //     {
        //         return NotFound();
        //     }
        //     ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", venda.ClienteId);
        //     ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Id", venda.FuncionarioId);
        //     return View(venda);
        // }
        
        

        // GET: Vendas/Create

        // POST: Vendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        // // POST: Vendas/Edit/5
        // // To protect from overposting attacks, enable the specific properties you want to bind to.
        // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, [Bind("Id,Data,TotalVendas,QuantidadeProdutos,Pagamento,ClienteId,FuncionarioId")] Vendas venda)
        // {
        //     if (id != venda.Id)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(venda);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!VendaExists(venda.Id))
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
        //     ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", venda.ClienteId);
        //     ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Id", venda.FuncionarioId);
        //     return View(venda);
        // }
        // GET: Vendas/Details/5
        // public async Task<IActionResult> Details(int id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var venda = await _vendasServvice.BuscarId(id);
        //     if (venda == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(venda);
        // }

        // // GET: Vendas/Delete/5
        // public async Task<IActionResult> Delete(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var venda = await _context.Vendas
        //         .Include(v => v.Cliente)
        //         .Include(v => v.Funcionario)
        //         .FirstOrDefaultAsync(m => m.Id == id);
        //     if (venda == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(venda);
        // }

        // // POST: Vendas/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(int id)
        // {
        //     var venda = await _context.Vendas.FindAsync(id);
        //     if (venda != null)
        //     {
        //         _context.Vendas.Remove(venda);
        //     }

        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }

        // private bool VendaExists(int id)
        // {
        //     return _context.Vendas.Any(e => e.Id == id);
        // }
    }
}
