using System.Collections.Generic;
using System.Threading.Tasks;
using Plantech.DTOs;

namespace Plantech.Interfaces
{
    public interface IPlantioService
    {
        Task<IEnumerable<PlantioDTO>> GetAllAsync();
        Task<PlantioDTO> GetByIdAsync(int id);

        
        Task UpdateAsync(PlantioDTO plantioDto);
        Task DeleteAsync(int id);



        Task CreatePlantioAsync(PlantioDTO plantioDto);
        Task CreateInsumosPlantioAsync(InsumosPlantioDTO insumoPlantioDto);

        Task<PlantioDTO> GetUltimoPlantioAsync();


        Task<IEnumerable<FuncionarioDTO>> GetFuncionariosAsync();

        Task<IEnumerable<LotesInsumoDTO>> GetLotesInsumosAsync();

    }
}
