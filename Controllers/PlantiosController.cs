using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Plantech.ViewModels;
using Plantech.DTOs;
using Plantech.Interfaces;
using AutoMapper;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Plantech.Controllers
{

    [Authorize(Roles = "Agricultor, Administrador")]
    public class PlantiosController(IPlantioService plantioService, IMapper mapper, IUsuarioService usuarioService) : Controller
    {
        private readonly IPlantioService _plantioService = plantioService;
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

            var userId = int.Parse(userIdClaim.Value);
            var usuario = await _usuarioService.GetByIdAsync(userId);
            var funcionario = usuario.Funcionarios.FirstOrDefault(); // Pega o primeiro funcionário associado ao usuário
            return funcionario != null ? funcionario.Id : 0;
        }





        // GET: Plantios
        public async Task<IActionResult> Index()
        {
            var plantios = await _plantioService.GetAllAsync();
            var viewModels = _mapper.Map<IEnumerable<PlantioViewModel>>(plantios);
            return View(viewModels);
        }

        // GET: Plantios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantio = await _plantioService.GetByIdAsync(id.Value);
            if (plantio == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<PlantioViewModel>(plantio);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new PlantioViewModel();
            var funcionarioId = await GetLoggedFuncionarioId();
            var usuario = await _usuarioService.GetByIdAsync(funcionarioId);
            var funcionarioNome = usuario.Funcionarios.FirstOrDefault()?.Nome;

            Console.WriteLine($"FuncionarioId: {funcionarioId}");
            Console.WriteLine($"FuncionarioNome: {funcionarioNome}");

            model.FuncionarioId = funcionarioId;
            model.FuncionarioNome = funcionarioNome;

            ViewData["FuncionarioId"] = funcionarioId;
            ViewData["HortalicaId"] = new SelectList(await _plantioService.GetHortalicasAsync(), "Id", "Nome");
            ViewData["InsumosPlantios"] = new SelectList(await _plantioService.GetLotesInsumosAsync(), "Id", "Nome");

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataPlantio,HortalicaId,FuncionarioId,Quantidade")] PlantioViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.DataPlantio = DateOnly.FromDateTime(DateTime.Now); // Data atual
                viewModel.FuncionarioId = await GetLoggedFuncionarioId(); // Método para obter o ID do funcionário logado

                var plantioDTO = _mapper.Map<PlantioDTO>(viewModel);

                try
                {
                    Console.WriteLine("Sera que foi?");
                    await _plantioService.CreatePlantioWithInsumosAsync(plantioDTO);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    viewModel.ErrorMessage = ex.Message;
                }
            }

            ViewData["HortalicaId"] = new SelectList(await _plantioService.GetHortalicasAsync(), "Id", "Nome");
            ViewData["InsumosPlantios"] = new SelectList(await _plantioService.GetLotesInsumosAsync(), "Id", "Nome");

            return View(viewModel);
        }
    }
}
