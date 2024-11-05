using Microsoft.EntityFrameworkCore;
using Plantech.Data;
using Plantech.Interfaces;
using Plantech.Models;

public class PlantioRepository(PlantechContext context) : IPlantioRepository
{
    private readonly PlantechContext _context = context;
    
    public async Task<Plantio> GetUltimoPlantioAsync()
    {
        return await _context.Plantios
            .OrderByDescending(p => p.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Plantio>> GetAllAsync()
    {
        return await _context.Plantios.Include(p => p.Hortalica).ToListAsync();
    }


    public async Task<Plantio> GetByIdAsync(int id)
    {
        return await _context.Plantios.FindAsync(id);
    }

    public async Task UpdateAsync(Plantio plantio)
    {
        if (PlantioExists(plantio.Id))
        {
            _context.Plantios.Update(plantio);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        if (PlantioExists(id))
        {
            var plantio = await _context.Plantios.FindAsync(id);
            if (plantio != null)
            {
                _context.Plantios.Remove(plantio);
                await _context.SaveChangesAsync();
            }
        }
    }

    public async Task CreatePlantioAsync(Plantio plantio)
    {
        await _context.Plantios.AddAsync(plantio);
        await _context.SaveChangesAsync(); 
    }

    public async Task CreateInsumosPlantioAsync(InsumosPlantio insumoPlantio)
    {
        await _context.InsumosPlantios.AddAsync(insumoPlantio);
        await _context.SaveChangesAsync(); 
    }

    private bool PlantioExists(int id)
    {
        return _context.Plantios.Any(e => e.Id == id);
    }

    // Lotes de insumo
    public async Task<IEnumerable<LotesInsumo>> GetLotesInsumosAsync()
    {
        return await _context.LotesInsumos.ToListAsync();
    }

}
