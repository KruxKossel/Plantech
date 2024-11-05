using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plantech.Models;

namespace Plantech.Interfaces
{
    public interface IColheitaRepository
    {
        Task<IEnumerable<Colheita>> GetAllAsync();

        Task<Colheita> GetByIdAsync(int id);
        Task UpdateAsync(Colheita colheita);
        Task DeleteAsync(int id);


        Task CreateColheitaAsync(Colheita colheita);

    }
}