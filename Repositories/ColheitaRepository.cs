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
    public class ColheitaRepository(PlantechContext context) : IColheitaRepository
    {
        private readonly PlantechContext _context = context;

        public async Task CreateColheitaAsync(Colheita colheita)
        {
            await _context.Colheitas.AddAsync(colheita);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var colheita = await _context.Colheitas.FindAsync(id);
            if (colheita != null)
            {
                _context.Colheitas.Remove(colheita);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Colheita>> GetAllAsync()
        {
           return await _context.Colheitas .Include(c => c.Funcionario) .Include(c => c.LoteHortalica) .Include(c => c.LoteInsumo) .Include(c => c.Plantio) .ToListAsync();
        }

        public async Task<Colheita> GetByIdAsync(int id)
        {
            return await _context.Colheitas
                .Include(c => c.Funcionario)
                .Include(c => c.LoteHortalica)
                .Include(c => c.LoteInsumo)
                .Include(c => c.Plantio)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateAsync(Colheita colheita)
        {
            if (ColheitaExists(colheita.Id))
            {
                _context.Colheitas.Update(colheita);
                await _context.SaveChangesAsync();
            }
        }

        private bool ColheitaExists(int id)
        {
            return _context.Colheitas.Any(e => e.Id == id);
        }
    }
}