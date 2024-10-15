using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plantech.DTOs;

namespace Plantech.Interfaces
{
    public interface IPlantioService
{
    Task<IEnumerable<PlantioDTO>> GetAllAsync();
    Task<PlantioDTO> GetByIdAsync(int id);
    Task CreateAsync(PlantioDTO plantioDto);
    Task UpdateAsync(PlantioDTO plantioDto);
    Task DeleteAsync(int id);
}

}