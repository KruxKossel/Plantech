using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize(Roles = "Agricultor, Administrador")]
    public class ColheitasController(IColheitaService colheitaService, IMapper mapper, IUsuarioService usuarioService, 
                                        IHortalicaService hortalicaService, IFuncionarioService funcionarioService,
                                        ILotesHortalicasService lotesHortalicasService, ILotesInsumosService lotesInsumosService,
                                        IPlantioService plantioService) : Controller
    {
        private readonly IColheitaService _colheitaService = colheitaService;

        private readonly IMapper _mapper = mapper;

        private readonly IUsuarioService _usuarioService = usuarioService; // Adicione o serviço de usuário

        private readonly IHortalicaService _hortalicaService = hortalicaService;
        private readonly IFuncionarioService _funcionarioService = funcionarioService;
        private readonly ILotesHortalicasService _lotesHortalicasSerivce = lotesHortalicasService;
        private readonly ILotesInsumosService _lotesInsumosService = lotesInsumosService;
        private readonly IPlantioService _plantioService = plantioService;




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

        // GET: Colheitas
        public async Task<IActionResult> Index()
        {
            var colheitas = await _colheitaService.GetAllAsync();
            var viewModels = _mapper.Map<IEnumerable<ColheitaViewModel>>(colheitas);

            // Buscando os dados adicionais (nomes)
            var funcionarios = await _funcionarioService.GetFuncionariosAsync();
            var lotesHortalica = await _lotesHortalicasSerivce.ListarLotes();
            var lotesInsumo = await _lotesInsumosService.ListarLotes();
            var plantios = await _plantioService.GetAllAsync();

            // Preparando os dicionários para mapear os nomes
            var funcionarioNomes = funcionarios.ToDictionary(f => f.Id, f => f.Nome);
            var loteHortalicaNomes = lotesHortalica.ToDictionary(lh => lh.Id, lh => lh.Nome);
            var loteInsumoNomes = lotesInsumo.ToDictionary(li => li.Id, li => li.Nome);
            var plantioHortalicaNomes = plantios.ToDictionary(p => p.Id, p => p.Hortalica.Nome);

            // Passando os dicionários para o ViewData
            ViewData["FuncionarioNomes"] = funcionarioNomes;
            ViewData["LoteHortalicaNomes"] = loteHortalicaNomes;
            ViewData["LoteInsumoNomes"] = loteInsumoNomes;
            ViewData["PlantioHortalicaNomes"] = plantioHortalicaNomes;
            return View(viewModels);
        }


        // GET: Colheitas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colheita = await _colheitaService.GetByIdAsync(id.Value);
            if (colheita == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<ColheitaViewModel>(colheita);

            // Buscando os dados adicionais (nomes e quantidades)
            var funcionarios = await _funcionarioService.GetFuncionariosAsync();
            var lotesHortalica = await _lotesHortalicasSerivce.ListarLotes();
            var lotesInsumo = await _lotesInsumosService.ListarLotes();
            var plantios = await _plantioService.GetAllAsync();

            // Preparando os dicionários para mapear os nomes e quantidades
            var funcionarioNomes = funcionarios.ToDictionary(f => f.Id, f => f.Nome);
            var loteHortalicaNomes = lotesHortalica.ToDictionary(
                lh => lh.Id, 
                lh => new { Text = $"{lh.Nome} - Quantidade: {lh.Quantidade}" }
            );
            var loteInsumoNomes = lotesInsumo.ToDictionary(
                li => li.Id, 
                li => new { Text = $"{li.Nome} - Quantidade: {li.Quantidade}" }
            );
            var plantioHortalicaNomes = plantios.ToDictionary(
                p => p.Id, 
                p => $"{p.Hortalica.Nome} - Plantado em {p.DataPlantio?.ToString("dd/MM/yyyy")} - Quantidade plantada: {p.Quantidade}"
            );

            // Adicionando logs para verificar os dados
            System.Diagnostics.Debug.WriteLine("Dados da Colheita:");
            System.Diagnostics.Debug.WriteLine($"ID: {viewModel.Id}");
            System.Diagnostics.Debug.WriteLine($"Lote Hortalica ID: {viewModel.LoteHortalicaId}");
            System.Diagnostics.Debug.WriteLine($"Lote Insumo ID: {viewModel.LoteInsumoId}");
            
            System.Diagnostics.Debug.WriteLine("Funcionario Nomes:");
            foreach (var fn in funcionarioNomes)
            {
                System.Diagnostics.Debug.WriteLine($"{fn.Key}: {fn.Value}");
            }

            System.Diagnostics.Debug.WriteLine("Lote Hortalica Nomes:");
            foreach (var lh in loteHortalicaNomes)
            {
                System.Diagnostics.Debug.WriteLine($"{lh.Key}: {lh.Value.Text}");
            }

            System.Diagnostics.Debug.WriteLine("Lote Insumo Nomes:");
            foreach (var li in loteInsumoNomes)
            {
                System.Diagnostics.Debug.WriteLine($"{li.Key}: {li.Value.Text}");
            }

            // Passando os dicionários para o ViewData
            ViewData["FuncionarioNomes"] = funcionarioNomes;
            ViewData["LoteHortalicaNomes"] = loteHortalicaNomes;
            ViewData["LoteInsumoNomes"] = loteInsumoNomes;
            ViewData["PlantioHortalicaNomes"] = plantioHortalicaNomes;

            return View(viewModel);
        }





        // GET: Colheitas/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            Console.WriteLine("Método GET Create chamado");


            var model = new ColheitaViewModel();

            // Obter id e nome do funcionário logado
            var funcionarioId = await GetLoggedFuncionarioId();
            var usuario = await _usuarioService.GetByIdAsync(funcionarioId);

            // Atribuir valores ao modelo
            model.FuncionarioId = funcionarioId;
            model.DataColheita = DateOnly.FromDateTime(DateTime.Now);


            ViewData["FuncionarioNome"] = usuario.Funcionarios.FirstOrDefault()?.Nome;

            // Obter plantios feitos nas duas semanas antes da data de colheita
            var plantios = await _colheitaService.GetPlantioAsync();
            var selectListItems = plantios
                .Where(p => p.Status == "não colhida" && p.Hortalica != null)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{p.Hortalica.Nome} - Plantado em {p.DataPlantio.Value:dd/MM/yyyy} - Quantidade plantada: {p.Quantidade}",
                    Group = new SelectListGroup { Name = p.Quantidade.ToString() }
                })
                .ToList();

            Console.WriteLine($"Total de plantios não colhidos: {selectListItems.Count}");

            ViewData["Plantios"] = selectListItems;

            Console.WriteLine($"\n\nFuncionarioId: {model.FuncionarioId}");
            Console.WriteLine($"DataColheita: {model.DataColheita}\n\n");

            return View(model);
        }






        // POST: Colheitas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Quantidade,DataColheita,FuncionarioId,PlantioId")] ColheitaViewModel model)
        {
            Console.WriteLine($"\n\nFuncionarioId: {model.FuncionarioId}");
            Console.WriteLine($"DataColheita: {model.DataColheita}\n\n");
            Console.WriteLine($"Plantio: {model.PlantioId}\n\n");

            Console.WriteLine("Método POST Create chamado");

            
            

            // Validar o modelo
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Key: {state.Key}, Error: {error.ErrorMessage}");
                    }
                }

                // Recarregar os dados necessários se o modelo não for válido
                await LoadSelectListItemsAsync(model);
                return View(model);
            }

            // Mapeamento do modelo para DTO
            var colheitaDto = _mapper.Map<ColheitaDTO>(model);

            try
            {
                // Chamada do serviço para criação da colheita
                await _colheitaService.CreateColheitaAsync(colheitaDto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                await LoadSelectListItemsAsync(model); // Recarregar os dados em caso de erro
                return View(model);
            }
        }

        // Método auxiliar para carregar os itens da lista de seleção
        private async Task LoadSelectListItemsAsync(ColheitaViewModel  model)
        {

            // Obter id e nome do funcionário logado
            var funcionarioId = await GetLoggedFuncionarioId();
            var usuario = await _usuarioService.GetByIdAsync(funcionarioId);

            // Atribuir valores ao modelo
            model.FuncionarioId = funcionarioId;
            
            model.DataColheita = DateOnly.FromDateTime(DateTime.Now);

            ViewData["FuncionarioNome"] = usuario.Funcionarios.FirstOrDefault()?.Nome;

            // Obter plantios feitos nas duas semanas antes da data de colheita
            var plantios = await _colheitaService.GetPlantioAsync();
            var selectListItems = plantios
                .Where(p => p.Status == "não colhida" && p.Hortalica != null)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{p.Hortalica.Nome} - Plantado em {p.DataPlantio.Value:dd/MM/yyyy} - Quantidade plantada: {p.Quantidade}",
                    Group = new SelectListGroup { Name = p.Quantidade.ToString() }
                })
                .ToList();

            Console.WriteLine($"Total de plantios não colhidos: {selectListItems.Count}");

            ViewData["Plantios"] = selectListItems;

            Console.WriteLine($"\n\nFuncionarioId: {model.FuncionarioId}");
            Console.WriteLine($"DataColheita: {model.DataColheita}\n\n");
        }



        // GET: Colheitas/Edit/5
        // public async Task<IActionResult> Edit(int? id)
        // {

            
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var colheita = await _colheitaService.GetByIdAsync(id.Value);
        //     if (colheita == null)
        //     {
        //         return NotFound();
        //     }

        //     var model = _mapper.Map<ColheitaViewModel>(colheita);

        //     return View(model);
        // }

        // POST: Colheitas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, [Bind("Quantidade,DataColheita,FuncionarioId,LoteHortalicaId,LoteInsumoId,PlantioId")] ColheitaViewModel model)
        // {

        //     if (!ModelState.IsValid)
        //     {
        //         foreach (var state in ModelState)
        //         {
        //             foreach (var error in state.Value.Errors)
        //             {
        //                 Console.WriteLine($"Key: {state.Key}, Error: {error.ErrorMessage}");
        //             }
        //         }
        //     }

        //     if (id != model.Id)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             var colheita = _mapper.Map<ColheitaDTO>(model);
        //             await _colheitaService.UpdateAsync(colheita);
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!ColheitaExists(model.Id))
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

        //     return View(model);
        // }

        // GET: Colheitas/Delete/5
        // public async Task<IActionResult> Delete(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var colheita = await _colheitaService.GetByIdAsync(id.Value);
        //     if (colheita == null)
        //     {
        //         return NotFound();
        //     }

        //     var colheitaViewModel = _mapper.Map<ColheitaViewModel>(colheita);
        //     return View(colheitaViewModel);
        // }


        // POST: Colheitas/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(int id)
        // {
        //     if (!ColheitaExists(id))
        //     {
        //         return NotFound();
        //     }

        //     try
        //     {
        //         await _colheitaService.DeleteAsync(id);
        //     }
        //     catch (Exception ex)
        //     {
        //         // Log the exception or handle it as needed
        //         throw;
        //     }

        //     return RedirectToAction(nameof(Index));
        // }

        private bool ColheitaExists(int id)
        {
            return _colheitaService.GetByIdAsync(id) != null;
        }

    }
}
