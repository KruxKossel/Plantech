using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plantech.Data;
using Plantech.Enuns;
using Plantech.Interfaces;
using Plantech.Models;

namespace Plantech.Repositories
{
    public class ClienteRepository(PlantechContext context) : IClienteRepository
    {
        private readonly PlantechContext _context = context;

        public async Task Adicionar(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }
        public async Task AtualizarDadosAsync(Cliente cliente)
        {
            var local = _context.Set<Cliente>().Local.FirstOrDefault(entry => entry.Id.Equals(cliente.Id));
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cliente>> Listar()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task AtualizarStatusAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                cliente.Status = cliente.Status == "ativo" ? "inativo" : "ativo";
                _context.Clientes.Update(cliente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Cliente>> BuscarPorRazaoSocial(string razaoSocial)
        {
           return await _context.Clientes.Where(c => c.RazaoSocial.Contains(razaoSocial)).ToListAsync();
        }

        public async Task<Cliente> BuscarPorId(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }
    }
}


