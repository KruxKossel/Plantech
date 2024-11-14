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
        private readonly IOrdensCompraService _ordensCompraService;
        private readonly IMapper _mapper;
        private readonly IInsumoService _insumosService;
        private readonly IInsumoCompraService _insumosCompraService;
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



        public OrdensComprasController(PlantechContext context, IOrdensCompraService ordensCompraService, 
                                        IMapper mapper,IUsuarioService usuarioService, IInsumoService insumosService,
                                        IInsumoCompraService insumosCompraService)
        {
            _ordensCompraService = ordensCompraService;
            _mapper = mapper;
            _usuarioService = usuarioService;
            _insumosService = insumosService;
            _insumosCompraService = insumosCompraService;
        }

        
        
        
        [Authorize(Roles = "Comprador, Administrador")] 
        public async Task<IActionResult> Index()
        {
            await _ordensCompraService.DeletarTuplasZeradas();
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
            

            var usuarioLogadoID =  await GetLoggedFuncionarioId();
                for(int i =0; i<100 ; i++)
                Console.WriteLine(DateOnly.FromDateTime(DateTime.Now));

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
                ordensCompraDTO.FuncionarioId= usuarioLogadoID;
                var novaCompra = await _ordensCompraService.CriarCompra(ordensCompraDTO);
                // var novaCompra = ordensCompraDTO;
                    // for(int i = 0 ; i < 100 ; i++)
                    // Console.WriteLine($"NOVA ORDEM");
                    // Console.WriteLine($"{ordensCompraDTO.Id}");
                    // Console.WriteLine($"{ordensCompraVM.Id}");
                return RedirectToAction(nameof(AdicionarInsumo), new { id = novaCompra, idFornecedor = fornecedor.Id }); 
            
            }
            return View(ordensCompraVM);
        }

 
 
 
 
 
 
 
 
 
 
 
        [HttpGet("OrdensCompras/AdicionarInsumo/{id}/{idFornecedor}")]
        public async Task<IActionResult> AdicionarInsumo(int id, int idFornecedor)
        {
            try
            {

                var insumosDTO = await _insumosService.ListarAsync();
                var insumosViewModel = _mapper.Map<List<InsumoViewModel>>(insumosDTO);
                var model = new AdicionarInsumoViewModel
                {
                    OrdemCompraId = id,
                    FornecedorId = idFornecedor,
                    InsumosDisponiveis = insumosViewModel
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao carregar a página. Tente novamente mais tarde.");
            }
        }
        
        
        
        [HttpPost("OrdensCompras/AdicionarInsumo/{id}/{idFornecedor}")]
        public async Task<IActionResult> AdicionarInsumo(AdicionarInsumoViewModel model)
        {
            Console.WriteLine($"ID DA ORDEM DA COMPRA: {model.OrdemCompraId}");
                    if (!ModelState.IsValid)
                    {
                        var insumo = await _insumosService.ListarAsync();
                        model.InsumosDisponiveis = _mapper.Map<List<InsumoViewModel>>(insumo);
                        return View(model);
                    }
            var listainsumoscompraDTO = new List<InsumosCompraDTO>();

            var insumosSelecionados =  model.DadosInsumos.Where(d => d.Selecionado).ToList();
            if (model.DadosInsumos != null && model.DadosInsumos.Any())
            {
                Console.WriteLine("DADOS DOS INSUMOS:");
                foreach (var insumoDados in insumosSelecionados)
                {
                    var insumoCompraDTO = new InsumosCompraDTO
                    {
                        OrdemCompraId = model.OrdemCompraId,
                        InsumoId = insumoDados.InsumoId,
                        Quantidade = insumoDados.QtdInsumos,
                        PrecoUnitario = insumoDados.PrecoUnitario,
                        DataChegada = DateOnly.FromDateTime(DateTime.Now)
                    };

                    listainsumoscompraDTO.Add(insumoCompraDTO);

                    Console.WriteLine($"- ID: {insumoDados.InsumoId}, Quantidade: {insumoDados.QtdInsumos}, Preço Unitário: {insumoDados.PrecoUnitario}");
                }
            }
            else
            {
                Console.WriteLine("Nenhum dado de insumo.");
            }

            await _ordensCompraService.AdicionarInsumo(listainsumoscompraDTO);

            return RedirectToAction(nameof(Index), new { id = model.OrdemCompraId });
        }


        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var ordensCompra = await _ordensCompraService.GetOrdensCompraId(id);
            if (ordensCompra == null)
            {
                return NotFound();
            }

            var insumosCompra = await _ordensCompraService.DetalharCompra(id);

            var viewModel = new CompraDetalhadaViewModel
            {
                Id = ordensCompra.Id,
                Total = ordensCompra.Total,
                Status = ordensCompra.Status,
                FuncionarioId = ordensCompra.FuncionarioId,
                FornecedorId = ordensCompra.FornecedorId,
                DataCompra = ordensCompra.DataCompra,
                Fornecedor = ordensCompra.Fornecedor,
                Funcionario = ordensCompra.Funcionario,
                insumoscompra = insumosCompra.ToList()
            };

            return View(viewModel);
        }

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

        
                
        [Authorize(Roles = "Administrador, Comprador")]
        [HttpPost, ActionName("MudarStatus")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MudarStatus(int id)
        {
            var insumo = _ordensCompraService.AtualizarStatus(id);
            return RedirectToAction(nameof(Index));
        }
        
        
        
        
        
        
        
        // POST: OrdensCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ordensCompra = await _ordensCompraService.GetOrdensCompraId(id);
            if (ordensCompra != null)
            {
                await _ordensCompraService.DeletarCompra(id);
            }
            return RedirectToAction(nameof(Index));
        }

        // private bool OrdensCompraExists(int id)
        // {
        //     return _context.OrdensCompras.Any(e => e.Id == id);
        // }
    }
}
