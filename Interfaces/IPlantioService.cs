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
        Task CreatePlantioWithInsumosAsync(PlantioDTO plantioDto);


        Task<IEnumerable<FuncionarioDTO>> GetFuncionariosAsync();
        Task<IEnumerable<HortalicaDTO>> GetHortalicasAsync();

        Task<IEnumerable<LotesInsumoDTO>> GetLotesInsumosAsync();

    }
}
