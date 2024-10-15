using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plantech.Data;
using Plantech.Interfaces;
using Plantech.Models;

namespace Plantech.Repositories
{
    public class PlantioRepository(PlantechContext context) : IPlantioRepository
{
    private readonly PlantechContext _context = context;

        public async Task<IEnumerable<Plantio>> GetAllAsync()
    {
        return await _context.Plantios.ToListAsync();
    }

    public async Task<Plantio> GetByIdAsync(int id)
    {
        return await _context.Plantios.FindAsync(id);
    }

    public async Task AddAsync(Plantio plantio)
    {
        await _context.Plantios.AddAsync(plantio);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Plantio plantio)
    {
        _context.Plantios.Update(plantio);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var plantio = await _context.Plantios.FindAsync(id);
        if (plantio != null)
        {
            _context.Plantios.Remove(plantio);
            await _context.SaveChangesAsync();
        }
    }
}

}