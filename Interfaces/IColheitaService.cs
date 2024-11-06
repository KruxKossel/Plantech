using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plantech.DTOs;

namespace Plantech.Interfaces
{
    public interface IColheitaService
    {
        Task<IEnumerable<ColheitaDTO>> GetAllAsync();
        
        Task<ColheitaDTO> GetByIdAsync(int id);
        Task UpdateAsync(ColheitaDTO colheitaDto);
        Task DeleteAsync(int id);


        Task CreateColheitaAsync(ColheitaDTO colheitaDto);

        Task<IEnumerable<PlantioDTO>> GetPlantioAsync();

        Task<IEnumerable<FuncionarioDTO>> GetFuncionariosAsync();


    }
}