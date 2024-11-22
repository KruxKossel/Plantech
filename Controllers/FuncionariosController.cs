using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Plantech.DTOs;
using Plantech.Interfaces;
using Plantech.Models;
using Plantech.ViewModels;

namespace Plantech.Controllers;


[Authorize(Roles = "Administrador")]
public class FuncionariosController(IMapper mapper, IFuncionarioService funcionarioService, IUsuarioService usuarioService) : Controller
{

    private readonly IFuncionarioService _funcionarioService = funcionarioService;

    private readonly IMapper _mapper = mapper;

    private readonly IUsuarioService _usuarioService = usuarioService;
    public async Task<IActionResult> Index()
    {
        var funcionarios = await _funcionarioService.GetFuncionariosAsync();
        var usuarios = await _usuarioService.GetAllAsync();
        // Remova a busca por cargos aqui se não for necessário
        // var cargos = await _funcionarioService.GetCargosAsync();

        var funcionariosViewModel = funcionarios.Select(funcionario =>
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == funcionario.UsuarioId);
            // Remova ou adapte esta linha se não for necessário mapear cargos
            // var cargo = cargos.FirstOrDefault(c => c.Id == funcionario.CargoId);

            var viewModel = _mapper.Map<FuncionarioViewModel>(funcionario);
            if (usuario != null)
            {
                viewModel.NomeUsuario = usuario.NomeUsuario;
                viewModel.Email = usuario.Email;
                // Adicione outras propriedades que precisa mapear
            }

            // Remova esta linha se não for necessário mapear cargos
            // _mapper.Map(cargo, viewModel);

            return viewModel;
        }).ToList();

        return View(funcionariosViewModel);
    }



    [HttpGet]
    public async Task<ActionResult> Create()
    {

        Console.WriteLine("\n\n Entrou no GET");

        var model = new FuncionarioCreateViewModel();

        var cargos = await _funcionarioService.GetCargosAsync();

        var cargosPossiveis = cargos
        .Where(c => c.Funcao == "Agricultor" || c.Funcao == "Comprador" || c.Funcao == "Vendedor")
        .Select(c => new
        {
            c.Id,
            c.Funcao,
            c.Descricao

        }).ToList();

        ViewData["CargosFiltro"] = new SelectList(cargosPossiveis, "Id", "Funcao");


        ViewData["NomeUsuario"] = model.NomeUsuario;

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(FuncionarioCreateViewModel model)
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

        // criar usuario, cargo e funcionário
        // puxar id de cargo e usuario após criados e add no dto de funcionario
        if (ModelState.IsValid)
        {
            var usuarioDto = _mapper.Map<UsuarioDTO>(model);
            var funcionarioDto = _mapper.Map<FuncionarioDTO>(model);
            try
            {


                await _usuarioService.CreateAsync(usuarioDto);

                var ultimoUsuario = await _usuarioService.GetUltimoUsuarioAsync();

                var usuarioId = ultimoUsuario.Id;





                funcionarioDto.CargoId = model.CargoId; // puxar da view
                funcionarioDto.UsuarioId = usuarioId; // puxar do banco


                await _funcionarioService.CreateFuncionarioAsync(funcionarioDto);


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
        }

        // Verifique o valor de CargoId recebido 
        System.Diagnostics.Debug.WriteLine($"CargoId: {model.CargoId}");
        Console.WriteLine($"CargoId: {model.CargoId}");

        var cargos = await _funcionarioService.GetCargosAsync();



        var cargosPossiveis = cargos
        .Where(c => c.Funcao == "Agricultor" || c.Funcao == "Comprador" || c.Funcao == "Vendedor")
        .Select(c => new
        {
            c.Id,
            c.Funcao,
            c.Descricao

        }).ToList();

        ViewData["CargosFiltro"] = new SelectList(cargosPossiveis, "Id", "Funcao");

        model.NomeUsuario = "Deltrano";

        ViewData["NomeUsuario"] = model.NomeUsuario;

        return View(model);
    }

    [HttpPost, ActionName("MudarStatus")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MudarStatus(int id)
    {
        await _usuarioService.MudarStatus(id);
        return RedirectToAction(nameof(Index));
    }


    // GET: Funcionarios/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var funcionario = await _funcionarioService.GetByIdAsync(id.Value);
        if (funcionario == null)
        {
            return NotFound();
        }

        var viewModel = _mapper.Map<FuncionarioDTO>(funcionario);
        return View(viewModel);
    }

    // GET: Funcionarios/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var funcionario = await _funcionarioService.GetByIdAsync(id);
        Console.WriteLine($"Funcionario ID: {funcionario.Id}, Cargo ID: {funcionario.CargoId}");



        if (funcionario == null)
        {
            return NotFound();
        }

        // Verificação de permissão
        if (funcionario.Cargo.Funcao == "Administrador")
        {
            return Forbid(); // ou Unauthorized() dependendo do seu caso de uso
        }








        var viewModel = _mapper.Map<FuncionarioCreateViewModel>(funcionario);
        Console.WriteLine($"ViewModel Funcionario ID: {viewModel.Id}, ViewModel Cargo ID: {viewModel.CargoId}");

        viewModel.Senha = funcionario.Usuario.Senha;
        viewModel.Email = funcionario.Usuario.Email;
        viewModel.NomeUsuario = funcionario.Usuario.NomeUsuario;

        viewModel.Id = funcionario.Id;

        var cargos = await _funcionarioService.GetCargosAsync();

        var cargosPossiveis = cargos
            .Where(c => c.Funcao == "Agricultor" || c.Funcao == "Comprador" || c.Funcao == "Vendedor")
            .Select(c => new
            {
                c.Id,
                c.Funcao,
                c.Descricao
            }).ToList();

        ViewData["CargosFiltro"] = new SelectList(cargosPossiveis, "Id", "Funcao");

        return View(viewModel);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, FuncionarioCreateViewModel viewModel)
    {
        if (id != viewModel.Id)
        {
            return NotFound();
        }

        // Verificação se o CargoId e UsuarioId existem antes de tentar atualizar
        var cargoExistente = await _funcionarioService.GetCargoByIdAsync(viewModel.CargoId);
        if (cargoExistente == null)
        {
            ModelState.AddModelError(nameof(viewModel.CargoId), "Cargo inválido ou inexistente.");
            return View(viewModel);
        }

        var usuarioExistente = await _usuarioService.GetByIdAsync(viewModel.UsuarioId);
        if (usuarioExistente == null)
        {
            ModelState.AddModelError(nameof(viewModel.UsuarioId), "Usuário inválido ou inexistente.");
            return View(viewModel);
        }

        var funcionario = await _funcionarioService.GetByIdAsync(id);
        if (funcionario == null || funcionario.Cargo.Funcao == "Administrador")
        {
            return Forbid(); // ou Unauthorized() dependendo do seu caso de uso
        }

        if (!ModelState.IsValid)
        {
            var cargos = await _funcionarioService.GetCargosAsync();
            var cargosPossiveis = cargos
                .Where(c => c.Funcao == "Agricultor" || c.Funcao == "Comprador" || c.Funcao == "Vendedor")
                .Select(c => new { c.Id, c.Funcao }).ToList();

            ViewData["CargosFiltro"] = new SelectList(cargosPossiveis, "Id", "Funcao");
            return View(viewModel);
        }

        var funcionarioDto = _mapper.Map<FuncionarioDTO>(viewModel);
        await _funcionarioService.UpdateAsync(funcionarioDto);

        var usuarioDto = _mapper.Map<UsuarioDTO>(viewModel);
        await _usuarioService.UpdateAsync(usuarioDto);
        return RedirectToAction(nameof(Index));
    }






    // fazer edit



}