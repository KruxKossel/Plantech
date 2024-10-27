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
    public class FornecedoresRepository(PlantechContext context) : IFornecedoresRepository
    {
        private readonly PlantechContext _context = context;

        public async Task AdicionarAsync(Fornecedore fornecedor)
        {
            await _context.Fornecedores.AddAsync(fornecedor);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarDadosAsync(Fornecedore fornecedor){
            var local = _context.Set<Fornecedore>().Local.FirstOrDefault(entry => entry.Id.Equals(fornecedor.Id));
            if(local != null){
                _context.Entry(local).State = EntityState.Detached;
            }
            _context.Entry(fornecedor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Fornecedore>> ListarAsync(){
            return await _context.Fornecedores.ToListAsync();
        }
        public async Task AtualizarStatusAsync(int id){
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if(fornecedor != null){
                fornecedor.Status = fornecedor.Status == "ativo" ? "inativo" : "ativo";
                _context.Fornecedores.Update(fornecedor);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Fornecedore> ObterPorIdAsync(int id){
            return await _context.Fornecedores.FindAsync(id);
        }

        public async Task<List<Fornecedore>> BuscarPorRazaoSocialAsync(string razaoSocial){
            return await _context.Fornecedores.Where(f => f.RazaoSocial.Contains(razaoSocial)).ToListAsync();
        }

    }
}