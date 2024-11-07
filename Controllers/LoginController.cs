using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Plantech.Interfaces;
using Plantech.ViewModels;
using Plantech.DTOs;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Plantech.Controllers
{
    public class LoginController(IUsuarioService usuarioService) : Controller
    {
        private readonly IUsuarioService _usuarioService = usuarioService;

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _usuarioService.AuthenticateAsync(model.Username, model.Password);
            if (user == null || user.Status != "ativo")
            {
                // Autenticação falhou ou usuário inativo
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
        public IActionResult Login()
        {
            return View();
        }

    //     [HttpGet]
    //     public IActionResult Create()
    //     {
    //         return View();
    //     }

    //     [HttpPost]
    //     public async Task<IActionResult> Create(UsuarioViewModel model)
    //     {
    //         if (ModelState.IsValid)
    //         {
    //             var usuarioDto = new UsuarioDTO
    //             {
    //                 NomeUsuario = model.NomeUsuario,
    //                 Email = model.Email,
    //                 Senha = model.Senha,
    //                 Status = model.Status
    //             };

    //             await _usuarioService.CreateAsync(usuarioDto);
    //             return RedirectToAction("Index", "Home");
    //         }
    //         return View(model);
    //     }
    }
}