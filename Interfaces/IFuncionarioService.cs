using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plantech.DTOs;

namespace Plantech.Interfaces
{
    public interface IFuncionarioService
    {
        Task<FuncionarioDTO> GetByIdAsync(int id);

        Task<IEnumerable<FuncionarioDTO>> GetFuncionariosAsync();

        // Task<IEnumerable<FuncionarioDTO>> ObterFuncionariosPorIdsAsync(List<int> ids);

        Task CreateFuncionarioAsync(FuncionarioDTO funcionariosDto);

        Task<IEnumerable<CargoDTO>> GetCargosAsync();

        Task<CargoDTO> GetCargoByIdAsync(int id);

         Task UpdateAsync(FuncionarioDTO funcionariosDto);
    }
}