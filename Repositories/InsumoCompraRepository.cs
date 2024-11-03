using Microsoft.EntityFrameworkCore;
using Plantech.Data;
using Plantech.Interfaces;
using Plantech.Models;

namespace Plantech.Repositories;

public class InsumoCompraRepository(PlantechContext context): IInsumoCompraRepository
{
    private readonly PlantechContext _context = context;

    public Task AdcionarInsumo(Insumo insumo)
    {
        throw new NotImplementedException();
    }

    public Task AlterarQuantidade(int id, int quantidade)
    {
        throw new NotImplementedException();
    }

    public async Task<InsumosCompra> GetInsumosCompraId(int id)
    {
        return await _context.InsumosCompras.FindAsync(id);
    }

    public async Task<List<InsumosCompra>> ListarInsumosCompra()
    {
        return await _context.InsumosCompras.Include(h => h.Lote).Include(h => h.OrdemCompra).ToListAsync();
    }

    public Task RemoverInsumo(int id)
    {
        throw new NotImplementedException();
    }
}
