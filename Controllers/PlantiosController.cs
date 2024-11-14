
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Plantech.ViewModels;
using Plantech.DTOs;
using Plantech.Interfaces;
using AutoMapper;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Plantech.Models;

namespace Plantech.Controllers
{

    [Authorize(Roles = "Agricultor, Administrador")]
    public class PlantiosController(IPlantioService plantioService, IMapper mapper, 
                                    IUsuarioService usuarioService, IHortalicaService  hortalicaService) : Controller
    {
        private readonly IPlantioService _plantioService = plantioService;

        private readonly IHortalicaService _hortalicaService = hortalicaService;
        private readonly IMapper _mapper = mapper;
        private readonly IUsuarioService _usuarioService = usuarioService; // Adicione o serviço de usuário

        private async Task<int> GetLoggedFuncionarioId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                Console.WriteLine("pqp");
                throw new InvalidOperationException("User is not authenticated.");
            }

            // Tente converter o valor para inteiro com validação
            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new FormatException($"O valor '{userIdClaim.Value}' não é um número válido.");
            }

            var usuario = await _usuarioService.GetByIdAsync(userId);
            var funcionario = usuario.Funcionarios.FirstOrDefault(); // Pega o primeiro funcionário associado ao usuário
            return funcionario != null ? funcionario.Id : 0;
        }






        // GET: Plantios
        public async Task<IActionResult> Index()
        {
            var plantios = await _plantioService.GetAllAsync();
            var viewModels = _mapper.Map<IEnumerable<PlantioIndexViewModel>>(plantios);

            ViewData["HortalicaId"] = new SelectList(await _hortalicaService.ListarHortalicasAsync(), "Id", "Nome");

            return View(viewModels);
        }

        // GET: Plantios/Details/5
        // public async Task<IActionResult> Details(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var plantio = await _plantioService.GetByIdAsync(id.Value);
        //     if (plantio == null)
        //     {
        //         return NotFound();
        //     }

        //     var viewModel = _mapper.Map<PlantioViewModel>(plantio);
        //     return View(viewModel);
        // }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new PlantioViewModel();

            // Obter id e nome do funcionário logado
            var funcionarioId = await GetLoggedFuncionarioId();
            var usuario = await _usuarioService.GetByIdAsync(funcionarioId);

            // Atribuir valores ao modelo
            model.FuncionarioId = funcionarioId;
            model.DataPlantio = DateOnly.FromDateTime(DateTime.Now);
            model.Status = "não colhida";

            ViewData["FuncionarioNome"] = usuario.Funcionarios.FirstOrDefault()?.Nome;

            // Obter hortaliças e lotes de insumos ativos
            ViewData["HortalicaId"] = new SelectList(await _hortalicaService.ListarHortalicasAsync(), "Id", "Nome");
            var lotesInsumos = await _plantioService.GetLotesInsumosAsync();
            var lotesAtivos = lotesInsumos
                .Where(l => l.Status == "ativo")
                .Select(l => new 
                {
                    l.Id,
                    l.Nome,
                    l.Quantidade,
                    DataValidade = l.DataValidade.HasValue ? l.DataValidade.Value.ToShortDateString() : "N/A"
                }).ToList();
                

            ViewData["LotesInsumo"] = lotesAtivos; // Passar os objetos diretamente
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlantioViewModel model, int[] SelectedInsumos, Dictionary<int, int> InsumosQuantities)
        {

            
            Console.WriteLine("\n\n Entrou no post");

            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Key: {state.Key}, Error: {error.ErrorMessage}");
                    }
                }
            }

            
            if (ModelState.IsValid)
            {

                Console.WriteLine("\n\n Entrou no if");
                var plantioDTO = _mapper.Map<PlantioDTO>(model);
                try
                {
                        Console.WriteLine("\n\n Entrou no Try");

                        await _plantioService.CreatePlantioAsync(plantioDTO);

                    // Obter o último plantio criado
                    var ultimoPlantio = await _plantioService.GetUltimoPlantioAsync();
                    int plantioId = ultimoPlantio.Id;

                    foreach (var insumoId in SelectedInsumos)
                    {
                        var quantidade = InsumosQuantities.ContainsKey(insumoId) ? InsumosQuantities[insumoId] : 0;

                        // Aqui, você pode criar o DTO de Insumos do plantio e salvá-lo no banco de dados
                        var insumoPlantioDTO = new InsumosPlantioDTO
                        {
                            PlantioId = plantioId,
                            LoteId = insumoId,
                            Quantidade = quantidade
                        };
                        
                        Console.WriteLine("\n\n Deu Certo krlh!!");
                        await _plantioService.CreateInsumosPlantioAsync(insumoPlantioDTO);
                    }
                    
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }

            }

            Console.WriteLine("\n\n porra");

            // Se houver erro, recarregar listas de hortaliças e lotes de insumos ativos
            // Obter id e nome do funcionário logado
            var funcionarioId = await GetLoggedFuncionarioId();
            var usuario = await _usuarioService.GetByIdAsync(funcionarioId);

            // Atribuir valores ao modelo
            model.FuncionarioId = funcionarioId;
            model.DataPlantio = DateOnly.FromDateTime(DateTime.Now);
            model.Status = "não colhida";

            ViewData["FuncionarioNome"] = usuario.Funcionarios.FirstOrDefault()?.Nome;

            // Obter hortaliças e lotes de insumos ativos
            ViewData["HortalicaId"] = new SelectList(await _hortalicaService.ListarHortalicasAsync(), "Id", "Nome");
            var lotesInsumos = await _plantioService.GetLotesInsumosAsync();
            var lotesAtivos = lotesInsumos
                .Where(l => l.Status == "ativo")
                .Select(l => new 
                {
                    l.Id,
                    l.Nome,
                    l.Quantidade,
                    DataValidade = l.DataValidade.HasValue ? l.DataValidade.Value.ToShortDateString() : "N/A"
                }).ToList();

            ViewData["LotesInsumo"] = lotesAtivos; // Passar os objetos diretamente

            return View(model);
        }

    }
}