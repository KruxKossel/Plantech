using System.Collections.Generic;
using System.Threading.Tasks;
using Plantech.Models;

namespace Plantech.Interfaces
{
    public interface IPlantioRepository
    public interface IPlantioRepository
    {
        Task<IEnumerable<Plantios>> GetAllAsync();
        Task<Plantios> GetByIdAsync(int id);
        Task UpdateAsync(Plantios plantio);
        Task DeleteAsync(int id);
        Task CreatePlantioAsync(Plantios plantio);

        Task CreateInsumosPlantioAsync(InsumosPlantios insumoPlantio);

        Task<Plantios> GetUltimoPlantioAsync();
        Task<IEnumerable<LotesInsumos>> GetLotesInsumosAsync();

    
    }
}
