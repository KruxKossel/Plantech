using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Plantech.DTOs;
using Plantech.Interfaces;
using Plantech.ViewModels;

namespace Plantech.Controllers
{
    
    public class InsumosController(IInsumoService insumoRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment) : Controller
    {
        private readonly IInsumoService _insumoService = insumoRepository;
        private readonly IMapper _mapper = mapper;

    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
        // GET: Insumos
        public async Task<IActionResult> Index(){
            
            var insumos = await _insumoService.ListarAsync();  
            return View(insumos);
        }

        // GET: Insumos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null){
                return NotFound();
            }

            var insumo = await _insumoService.ObterPorIdAsync(id);
                
            if (insumo == null)
            {
                return NotFound();
            }

            return View(insumo);
        }

 public async Task<IActionResult> Create()
    {
        var fornecedores = await _insumoService.ListarFornecedoresAsync(); 
        ViewData["FornecedorId"] = new SelectList(fornecedores, "Id", "Cnpj");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(InsumoViewModel insumoVM)
    {
        if (ModelState.IsValid)
        {
            string uniqueFileName = null;
            if (insumoVM.ImagemArquivo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + insumoVM.ImagemArquivo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await insumoVM.ImagemArquivo.CopyToAsync(fileStream);
                }
            }

            var insumoDto = _mapper.Map<InsumoDTO>(insumoVM);
            insumoDto.CaminhoImagem = uniqueFileName; 
            await _insumoService.CreateAsync(insumoDto);
            
            return RedirectToAction(nameof(Index));
        }

        return View(insumoVM);
    }
        // GET: Insumos/Edit/5
         public async Task<IActionResult> Edit(int id)
    {
        var insumo = await _insumoService.ObterPorIdAsync(id);
        if (insumo == null)
        {
            return NotFound();
        }
        var fornecedores = await _insumoService.ListarFornecedoresAsync(); 
        ViewData["FornecedorId"] = new SelectList(fornecedores, "Id", "Cnpj");
        var insumoVM = _mapper.Map<InsumoViewModel>(insumo);
        return View(insumoVM);
    }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InsumoViewModel insumoVM)
        {
            if (id != insumoVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string uniqueFileName = insumoVM.CaminhoImagem;
                if (insumoVM.ImagemArquivo != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + insumoVM.ImagemArquivo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await insumoVM.ImagemArquivo.CopyToAsync(fileStream);
                    }
                }
                var insumoDto = _mapper.Map<InsumoDTO>(insumoVM);
                insumoDto.CaminhoImagem = uniqueFileName;
                await _insumoService.AtualizarAsync(insumoDto);
                return RedirectToAction(nameof(Index));
            }
            var fornecedores = await _insumoService.ListarFornecedoresAsync();
            ViewData["FornecedorId"] = new SelectList(fornecedores, "Id", "Cnpj", insumoVM.FornecedorId);
            return View(insumoVM);
        }

        //     [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> EditImagem(int id, [Bind("Id, Nome, ImagemArquivo")] InsumoViewModel insumoVM)
        // {
        //     if (id != insumoVM.Id)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         string uniqueFileName = insumoVM.CaminhoImagem; 
        //         if (insumoVM.ImagemArquivo != null)
        //         {
        //             string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
        //             uniqueFileName = Guid.NewGuid().ToString() + "_" + insumoVM.ImagemArquivo.FileName;
        //             string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //             using (var fileStream = new FileStream(filePath, FileMode.Create))
        //             {
        //                 await insumoVM.ImagemArquivo.CopyToAsync(fileStream);
        //             }
        //         }

        //         var insumoDto = _mapper.Map<InsumoDTO>(insumoVM);
        //         insumoDto.CaminhoImagem = uniqueFileName;
        //         await _insumoService.AtualizarAsync(insumoDto);
        //         return RedirectToAction(nameof(Index));
        //     }

        //     return View(insumoVM);
        // }

        
        // [HttpGet]
        // public async Task<IActionResult> EditImagem(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var insumoDto = await _insumoService.ObterPorIdAsync(id.Value);
        //     if (insumoDto == null)
        //     {
        //         return NotFound();
        //     }

        //     var insumoVM = _mapper.Map<InsumoViewModel>(insumoDto);
        //     return View(insumoVM);
        // }   
        
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insumo = await _insumoService.ObterPorIdAsync(id);
            if (insumo == null)
            {
                return NotFound();
            }

            return View(insumo);
        }

        // POST: Insumos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insumo = await _insumoService.ObterPorIdAsync(id);
            if (insumo != null)
            {
                _insumoService.DeletarAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private  bool InsumoExists(int id)
        {
            bool retorno;
            var insumo =  _insumoService.ObterPorIdAsync(id);
            if(insumo !=null) 
            retorno = true; 
            else{retorno = false;}
            
            return retorno;
        }
    }
}
