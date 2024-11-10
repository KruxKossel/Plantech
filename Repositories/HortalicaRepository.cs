using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plantech.Data;
using Plantech.Interfaces;
using Plantech.Models;

namespace Plantech.Repositories
{
    public class HortalicaRepository(PlantechContext context) : IHortalicaRepository
    {
        private readonly PlantechContext _context = context;

        public async Task AdicionarAsync(Hortalicas hortalica)
        {
            await _context.Hortalicas.AddAsync(hortalica);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Hortalicas hortalica)
        {
            // Desanexar quaisquer entidades rastreadas com o mesmo ID
            var local = _context.Set<Hortalicas>()
                        .Local
                        .FirstOrDefault(entry => entry.Id.Equals(hortalica.Id));
            
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
            
            _context.Entry(hortalica).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task DeletarAsync(int id)
        {
            var hortalica = await _context.Hortalicas.FindAsync(id);
            if (hortalica != null)
            {
                _context.Hortalicas.Remove(hortalica);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Hortalicas> ObterPorIdAsync(int id)
        {
            return await _context.Hortalicas.FindAsync(id);
        }

        public async Task<List<Hortalicas>> ListarAsync()
        {
            return await _context.Hortalicas.ToListAsync();
        }

        public async Task AtualizarStatusAsync(int id)
        {
            var hortalica = await _context.Hortalicas.FindAsync(id);
            if(hortalica != null){
                hortalica.Status = hortalica.Status == "ativo" ? "inativo" : "ativo";
                _context.Hortalicas.Update(hortalica);
                await _context.SaveChangesAsync();
            }
        }
    }
}
