using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClienteController(IClienteService clienteService, IMapper mapper)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteService.ListarAsync();
            
            return View(clientes);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _clienteService.BuscarPorId(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cnpj,RazaoSocial,Endereco,Telefone,Email,Status")] ClienteViewModel clientevm)
        {
            if (ModelState.IsValid)
            {
                var clientedto = _mapper.Map<ClienteDTO>(clientevm);
                await _clienteService.AdicionarAsync(clientedto);
                return RedirectToAction(nameof(Index));
            }
            return View(clientevm);
        }


        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _clienteService.BuscarPorId(id);
            if (cliente == null)
            {
                return NotFound();
            }
            var clientevm = _mapper.Map<ClienteViewModel>(cliente);
            return View(clientevm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cnpj,RazaoSocial,Endereco,Telefone,Email,Status")] ClienteViewModel clientevm)
        {
            if (id != clientevm.Id)
            {
                return NotFound();
            }

            var clientedto = _mapper.Map<ClienteDTO>(clientevm);
            if (ModelState.IsValid)
            {
                try
                {
                    await _clienteService.AtualizarDadosAsync(clientedto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(clientevm.Id))
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
            var clienteVM = _mapper.Map<ClienteViewModel>(clientedto);
            return View(clienteVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientedto = _clienteService.AtualizarStatusAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            bool retorno;
            var fornecedor = _clienteService.BuscarPorId(id);
            if(fornecedor == null){
                retorno = false;
            }else{
                retorno = true;
            }
            
            return retorno;
        }
    }
}
