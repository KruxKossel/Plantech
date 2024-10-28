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
        Task AddPlantioWithInsumosAsync(Plantio plantio, List<InsumosPlantio> insumosPlantios);

        Task<IEnumerable<Funcionario>> GetFuncionariosAsync();
        Task<IEnumerable<Hortalica>> GetHortalicasAsync();

        Task<IEnumerable<LotesInsumo>> GetLotesInsumosAsync();

    
    }
}
