using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plantech.Models;

namespace Plantech.Interfaces
{
   public interface IPlantioRepository
    {
        Task<IEnumerable<Plantio>> GetAllAsync();
        Task<Plantio> GetByIdAsync(int id);
        Task AddAsync(Plantio plantio);
        Task UpdateAsync(Plantio plantio);
        Task DeleteAsync(int id);
    }

}