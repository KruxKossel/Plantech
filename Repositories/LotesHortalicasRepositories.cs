using Microsoft.EntityFrameworkCore;
using Plantech.Data;
using Plantech.Interfaces;
using Plantech.Models;

namespace Plantech.Repositories;

public class LotesHortalicasRepository(PlantechContext context): ILotesHortalicasRepository
{
    private readonly PlantechContext _context = context;

    public async Task EditarLote(LotesHortalicas lotesHortalica)
    {
                    var local = _context.Set<LotesHortalicas>()
                        .Local
                        .FirstOrDefault(entry => entry.Id.Equals(lotesHortalica.Id));
            
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
            
            _context.Entry(lotesHortalica).State = EntityState.Modified;
        
        // _context.LotesInsumos.Update(lotesInsumo);
        await _context.SaveChangesAsync();
    }

    public async Task<LotesHortalicas> GetLotesInsumoId(int id)
    {
        return  await _context.LotesHortalicas.Include(l => l.Hortalica).FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<LotesHortalicas>> ListarLotes()
    {
        return await _context.LotesHortalicas.Include(l => l.Hortalica).ToListAsync();
    }

}
