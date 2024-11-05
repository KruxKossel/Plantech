using System.Collections.Generic;
using System.Threading.Tasks;
using Plantech.Models;

namespace Plantech.Interfaces
{
    public interface IPlantioRepository
    {
        Task<IEnumerable<Plantio>> GetAllAsync();
        Task<Plantio> GetByIdAsync(int id);
        Task UpdateAsync(Plantio plantio);
        Task DeleteAsync(int id);
        Task CreatePlantioAsync(Plantio plantio);

        Task CreateInsumosPlantioAsync(InsumosPlantio insumoPlantio);

        Task<Plantio> GetUltimoPlantioAsync();
        Task<IEnumerable<LotesInsumo>> GetLotesInsumosAsync();

    
    }
}
