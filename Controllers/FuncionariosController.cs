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
        var cargos = await _funcionarioService.GetCargosAsync(); // remover do index e deixar só em detalhes

        var funcionariosViewModel = funcionarios.Select(funcionario =>
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == funcionario.UsuarioId);
            var cargo = cargos.FirstOrDefault(c => c.Id == funcionario.CargoId);

            var viewModel = _mapper.Map<FuncionarioViewModel>(funcionario);
            _mapper.Map(usuario, viewModel);
            _mapper.Map(cargo, viewModel);

            return viewModel;
        });

        return View(funcionariosViewModel);
    }


    [HttpGet]
    public async Task<ActionResult> Create(){

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

        model.NomeUsuario = "Deltrano";
       
        
        ViewData["NomeUsuario"] =  model.NomeUsuario;

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
        
        ViewData["NomeUsuario"] =  model.NomeUsuario;

        return View(model);
    }

        [Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("MudarStatus")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MudarStatus(int id)
        {
            await _usuarioService.MudarStatus(id);
            return RedirectToAction(nameof(Index));
        }

}