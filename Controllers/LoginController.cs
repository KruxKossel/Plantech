using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Plantech.Interfaces;
using Plantech.ViewModels;
using Plantech.DTOs;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Data;
using AutoMapper;

namespace Plantech.Controllers
{
    public class LoginController(IUsuarioService usuarioService, IMapper mapper, IFuncionarioService funcionarioService) : Controller
    {
        private readonly IUsuarioService _usuarioService = usuarioService;

        private readonly IFuncionarioService _funcionarioService = funcionarioService;

        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _usuarioService.AuthenticateAsync(model.Username, model.Password);
            if (user == null || user.Status != "ativo" )
            {
                // Autenticação falhou ou usuário inativo
                ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
                return View(model);
            }

            var funcionario = await _usuarioService.GetFuncionarioByUserIdAsync(user.Id);

            Console.WriteLine($"Funcionario: {funcionario?.Id}, Status: {funcionario?.Status}");
            if(funcionario == null || funcionario.Status != "ativo")
            {
                // Autenticação falhou ou funcionário inativo
                ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
                return View(model);
            }

            // Obter o cargo do usuário
            var cargo = await _usuarioService.GetCargoByUserIdAsync(user.Id);
            if (cargo == null)
            {
                ModelState.AddModelError(string.Empty, "Cargo do usuário não encontrado.");
                return View(model);
            }

            // Configuração das claims do usuário autenticado
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.NomeUsuario),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, cargo.Funcao), // Adiciona a função do usuário como claim
                new Claim("Status", user.Status)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // Mantenha o usuário autenticado
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            // Redirecionar baseado no cargo do usuário
            return cargo.Funcao switch
            {
                "Administrador" => RedirectToAction("Index", "Administrador"),
                "Agricultor" => RedirectToAction("Index", "Agricultor"),
                "Comprador" => RedirectToAction("Index", "Comprador"),
                "Vendedor" => RedirectToAction("Index", "Vendedor"),
                _ => View(model),
            };
        }

        public async Task<IActionResult> Logout()
        {
            // Deslogar o usuário
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var existeUser = await _usuarioService.GetUltimoUsuarioAsync();

            // Verifique se existeUser é nulo antes de acessar sua propriedade Id
            if (existeUser != null)
            {
                ViewData["ExiteUser"] = existeUser.Id;
            }
            else
            {
                ViewData["ExiteUser"] = null;
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var existeUser = await _usuarioService.GetUltimoUsuarioAsync();

            if (existeUser == null)
            {
                return View();
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Create(FuncionarioCreateViewModel model)
        {
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


                    return RedirectToAction(nameof(Login));
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
            }

            return View(model);

        }
    }
}