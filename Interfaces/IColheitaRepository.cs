using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plantech.Models;

namespace Plantech.Interfaces
{
    public interface IColheitaRepository
    {
        Task<IEnumerable<Colheitas>> GetAllAsync();

        Task<Colheitas> GetByIdAsync(int id);
        Task UpdateAsync(Colheitas colheita);
        Task DeleteAsync(int id);


        Task CreateColheitaAsync(Colheitas colheita);



        // Culturas e hortaliças perdidas

        Task<IEnumerable<CulturasPerdidas>> GetCulturasPerdidas();

        Task<IEnumerable<HortalicasPerdidas>> GetHortaPerdidas();



    }
}