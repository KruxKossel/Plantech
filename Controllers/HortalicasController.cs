using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Plantech.Interfaces;
using Plantech.ViewModels;
using Plantech.DTOs;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Plantech.Controllers
{
    [Authorize(Roles = "Agricultor, Administrador, Vendedor")]
    public class HortalicasController(IHortalicaService hortalicaService, IWebHostEnvironment webHostEnvironment) : Controller
    {
        private readonly IHortalicaService _hortalicaService = hortalicaService;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        // GET: Hortalicas
        [HttpGet]
        [Authorize(Roles = "Agricultor, Administrador, Vendedor")]
        public async Task<IActionResult> Index()
        {
            var hortalicas = await _hortalicaService.ListarHortalicasAsync();
            return View(hortalicas);
        }

        // GET: Hortalicas/Details/5
        [HttpGet]
        [Authorize(Roles = "Agricultor, Administrador, Vendedor")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hortalica = await _hortalicaService.ObterHortalicaPorIdAsync(id.Value);
            if (hortalica == null)
            {
                return NotFound();
            }

            return View(hortalica);
        }

        // GET: Hortalicas/Create
        [HttpGet]
        [Authorize(Roles = "Agricultor, Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hortalicas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
         [Authorize(Roles = "Agricultor, Administrador")]
        public async Task<IActionResult> Create(HortalicaViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.ImagemArquivo != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImagemArquivo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImagemArquivo.CopyToAsync(fileStream);
                    }
                }

                var hortalicaDto = new HortalicaDTO
                {
                    Nome = model.Nome,
                    Descricao = model.Descricao,
                    Observacoes = model.Observacoes,
                    CaminhoImagem = uniqueFileName
                };

                await _hortalicaService.CreateHortalicaAsync(hortalicaDto);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Hortalicas/Edit/5
        [HttpGet]
        [Authorize(Roles = "Agricultor, Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hortalica = await _hortalicaService.ObterHortalicaPorIdAsync(id.Value);
            if (hortalica == null)
            {
                return NotFound();
            }

            var model = new HortalicaViewModel
            {
                Id = hortalica.Id,
                Nome = hortalica.Nome,
                Descricao = hortalica.Descricao,
                Observacoes = hortalica.Observacoes,
                CaminhoImagem = hortalica.CaminhoImagem // Carregar o caminho da imagem existente
            };

            return View(model);
        }


        // POST: Hortalicas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Agricultor, Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Observacoes")] HortalicaViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var hortalicaDto = await _hortalicaService.ObterHortalicaPorIdAsync(model.Id);

                if (hortalicaDto == null)
                {
                    return NotFound();
                }

                hortalicaDto.Nome = model.Nome;
                hortalicaDto.Descricao = model.Descricao;
                hortalicaDto.Observacoes = model.Observacoes;
                // Não alterar o caminho da imagem aqui

                try
                {
                    await _hortalicaService.AtualizarHortalicaAsync(hortalicaDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await HortalicaExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Agricultor, Administrador")]
        public async Task<IActionResult> EditImagem(int id, [Bind("Id, Nome, ImagemArquivo")] HortalicaViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var hortalicaDto = await _hortalicaService.ObterHortalicaPorIdAsync(model.Id);

                if (hortalicaDto == null)
                {
                    return NotFound();
                }

                string uniqueFileName = null;
                if (model.ImagemArquivo != null && model.ImagemArquivo.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImagemArquivo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    try
                    {
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.ImagemArquivo.CopyToAsync(fileStream);
                        }
                        hortalicaDto.CaminhoImagem = uniqueFileName; // Atualizar o caminho da imagem
                        Console.WriteLine("Imagem salva com sucesso: " + uniqueFileName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao salvar a imagem: " + ex.Message);
                        ModelState.AddModelError("ImagemArquivo", "Ocorreu um erro ao salvar a imagem.");
                        return View(model);
                    }
                }

                try
                {
                    await _hortalicaService.AtualizarHortalicaAsync(hortalicaDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await HortalicaExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            foreach (var state in ModelState)
            {
                Console.WriteLine($"Chave: {state.Key}, Erros: {string.Join(",", state.Value.Errors.Select(e => e.ErrorMessage))}");
            }
            
            return View(model);
        }





        [HttpGet]
        [Authorize(Roles = "Agricultor, Administrador")]
        public async Task<IActionResult> EditImagem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hortalica = await _hortalicaService.ObterHortalicaPorIdAsync(id.Value);
            if (hortalica == null)
            {
                return NotFound();
            }

            var model = new HortalicaViewModel
            {
                Id = hortalica.Id,
                Nome = hortalica.Nome,
                CaminhoImagem = hortalica.CaminhoImagem // Carregar o caminho da imagem existente
            };

            return View(model);
        }



        // GET: Hortalicas/Delete/5
        [Authorize(Roles = "Agricultor, Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hortalica = await _hortalicaService.ObterHortalicaPorIdAsync(id.Value);
            if (hortalica == null)
            {
                return NotFound();
            }

            return View(hortalica);
        }

        // POST: Hortalicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Agricultor, Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _hortalicaService.DeletarHortalicaAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> HortalicaExists(int id)
        {
            var hortalica = await _hortalicaService.ObterHortalicaPorIdAsync(id);
            return hortalica != null;
        }
    }
}
