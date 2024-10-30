using Microsoft.EntityFrameworkCore;
using Plantech.Data;
using Plantech.DTOs;
using Plantech.Interfaces;
using Plantech.Models;

namespace Plantech.Repositories;

public class InsumoRepository(PlantechContext context): IInsumoRepository
{
    private readonly PlantechContext _context = context;

    public async Task AtualizarAsync(Insumo insumo)
    {
        var local = _context.Set<Insumo>().Local.FirstOrDefault(entry => entry.Id.Equals(insumo.Id));
        if(local != null){
            _context.Entry(local).State = EntityState.Detached;
        }
        _context.Entry(insumo).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeletarAsync(int id)
    {
            var insumos = await _context.Insumos.FindAsync(id);
            if (insumos != null)
            {
                _context.Insumos.Remove(insumos);
                await _context.SaveChangesAsync();
            }
    }


    public async Task CreateAsync(Insumo insumo)
    {
        await _context.Insumos.AddAsync(insumo);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Insumo>> ListarAsync()
    {
        return await _context.Insumos.Include(i => i.Fornecedor).ToListAsync();
    }

    public async Task<Insumo> ObterPorIdAsync(int id)
    {
        return await _context.Insumos.Include(i => i.Fornecedor).FirstOrDefaultAsync(i => i.Id ==id);
    }

    public async Task<InsumoDTO> ObterPorCnpjAsync(string cnpj)
    {
        return await _context.Insumos
            .Where(i => i.Fornecedor.Cnpj == cnpj) // Supondo que Insumo tem um Fornecedor com CNPJ
            .Select(i => new InsumoDTO { /* Mapeie os campos necessários */ })
            .FirstOrDefaultAsync();
    }

    public async Task<List<Fornecedore>> ListarFornecedoresAsync()
    {
        return await _context.Fornecedores.ToListAsync();
    }
}
