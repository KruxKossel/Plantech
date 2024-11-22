using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Plantech.DTOs;
using Plantech.Interfaces;
using Plantech.ViewModels;
using Newtonsoft.Json;

namespace Plantech.Controllers
{
    public class VendasController(IVendasService vendasServvice, IMapper mapper, IClienteService clienteService, IUsuarioService usuarioService, ILotesHortalicasService lotesHortalicasService) : Controller
    {
        // private readonly PlantechContext _context;
        private readonly IVendasService _vendasService = vendasServvice;
        private readonly IMapper _mapper = mapper;
        private readonly IClienteService _clienteService = clienteService;
        private readonly IUsuarioService _usuarioService = usuarioService;
        private readonly ILotesHortalicasService _lotesHortalicas = lotesHortalicasService;
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
            // Console.WriteLine("\n\n\n Entrou no Post \n\n\n");
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
                // Console.WriteLine($"\n\n\n Entrou no ModelState\n Id Cliente {vendavm.ClienteId},\n Status {vendavm.Status}\n Data {vendavm.Data} \n   \n\n\n");
                // var idvenda = await _vendasService.CriarVenda(vendadto);
                
                TempData["venda"] =JsonConvert.SerializeObject(vendavm);
                return RedirectToAction(nameof(AdicionarHortalica));
            }
            var clientes = await _clienteService.ListarAsync();
            var clientesVm = _mapper.Map<IEnumerable<ClienteViewModel>>(clientes);
            ViewData["ClienteId"] = new SelectList(clientesVm, "Id", "Cnpj");
            return View(vendavm);
        }

        [HttpGet("Vendas/NovaVenda/AdicionarHortalica")]
        public async Task<IActionResult> AdicionarHortalica(){
                var lotesHortalicaDTO = await _lotesHortalicas.ListarLotesAtivos();
                var lotes = _mapper.Map<List<LotesHortalicaViewModel>>(lotesHortalicaDTO); 

                var model = new AdicionarHortalicaViewModel{
                    LotesDisponiveis = lotes
                };
                return View(model);
        }
        
        [HttpPost("Vendas/NovaVenda/AdicionarHortalica")]
        public async Task<IActionResult> AdicionarHortalica(AdicionarHortalicaViewModel model)
        {
            Console.WriteLine("\n\n\n ENTROU NO POST ADICIONAR HORTALICA \n\n\n");

            if (TempData["venda"] == null)
            {
                return BadRequest("A venda não foi encontrada. A requisição é inválida.");
            }

            var venda = JsonConvert.DeserializeObject<VendaViewModel>((string)TempData["venda"]);
            venda.FuncionarioId = await GetLoggedFuncionarioId();

            Console.WriteLine("\n\n\n");
            Console.WriteLine("Id: " + venda.Id);
            Console.WriteLine("Data: " + (venda.Data.HasValue ? venda.Data.Value.ToString("yyyy-MM-dd") : "null"));
            Console.WriteLine("TotalVendas: " + (venda.TotalVendas.HasValue ? venda.TotalVendas.Value.ToString("F2") : "null"));
            Console.WriteLine("QuantidadeProdutos: " + (venda.QuantidadeProdutos.HasValue ? venda.QuantidadeProdutos.Value.ToString() : "null"));
            Console.WriteLine("Status: " + venda.Status);
            Console.WriteLine("ClienteId: " + venda.ClienteId);
            Console.WriteLine("FuncionarioId: " + venda.FuncionarioId);
            Console.WriteLine("\n\n\n");

            if (ModelState.IsValid)
            {
                if (model.LotesSelecionados != null && model.LotesSelecionados.Any())
                {
                    var vendaDTO = _mapper.Map<VendaDTO>(venda);
                    var vendaId = await _vendasService.CriarVenda(vendaDTO);

                    var hortalicasVendaList = model.LotesSelecionados.Select(loteSelecionado => new HortalicasVendaDTO
                    {
                        VendaId = vendaId,
                        LoteId = loteSelecionado.LoteId,
                        Quantidade = loteSelecionado.Quantidade,
                        PrecoUnitario = loteSelecionado.PrecoUnitario
                    }).ToList();

                    Console.WriteLine("\n\n\nLista de HortalicasVendaDTO:");
                    foreach (var item in hortalicasVendaList)
                    {
                            var Gambiarra =  await _lotesHortalicas.GetLotesInsumoId(item.LoteId);
                            item.PrecoUnitario = Gambiarra.PrecoVenda;
                            Console.WriteLine($"\n\n\nVendaId: {item.VendaId} \nLoteId: {item.LoteId}, \nQuantidade: {item.Quantidade}, \nPrecoUnitario: {item.PrecoUnitario}\n\n\n");
                    }

                    await _vendasService.AdicionarHortalica(hortalicasVendaList);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Carrega os lotes ativos se não houver lotes selecionados
                    var lotesHortalicaDTO = await _lotesHortalicas.ListarLotesAtivos();
                    var lotes = _mapper.Map<List<LotesHortalicaViewModel>>(lotesHortalicaDTO);
                    model.LotesDisponiveis = lotes;
                    return View(model);
                }
            }

            return BadRequest("Os dados do formulário não são válidos.");
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _vendasService.BuscarId(id);
            var detalhes = await _vendasService.DetalharVenda(id);
            var model = new VendaDetalhadaViewModel{
                Data = venda.Data,
                TotalVendas = venda.TotalVendas,
                Status = venda.Status,
                ClienteId = venda.ClienteId,
                FuncionarioId = venda.FuncionarioId,
                Cliente = venda.Cliente,
                Funcionario = venda.Funcionario,
                HortalicasVendas = detalhes.ToList()
            };


            if (venda == null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
            
}
        // GET: Vendas/Details/5
            


        
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
