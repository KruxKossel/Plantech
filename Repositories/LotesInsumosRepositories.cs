using Microsoft.EntityFrameworkCore;
using Plantech.Data;
using Plantech.Interfaces;
using Plantech.Models;

namespace Plantech.Repositories;

public class LotesInsumosRepository(PlantechContext context): ILotesInsumosRepository
{
    private readonly PlantechContext _context = context;

    public async Task EditarLote(LotesInsumo lotesInsumo)
    {
                    var local = _context.Set<LotesInsumo>()
                        .Local
                        .FirstOrDefault(entry => entry.Id.Equals(lotesInsumo.Id));
            
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
            
            _context.Entry(lotesInsumo).State = EntityState.Modified;
        
        // _context.LotesInsumos.Update(lotesInsumo);
        await _context.SaveChangesAsync();
    }

    public async Task<LotesInsumo> GetLotesInsumoId(int id)
    {
        return  await _context.LotesInsumos.Include(l => l.Insumo).FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<LotesInsumo>> ListarLotes()
    {
        return await _context.LotesInsumos.Include(l => l.Insumo).ToListAsync();
    }

}
