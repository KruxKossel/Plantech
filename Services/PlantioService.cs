using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plantech.DTOs;
using Plantech.Interfaces;
using Plantech.Models;

namespace Plantech.Services
{
    public class PlantioService(IPlantioRepository repository) : IPlantioService
{
    private readonly IPlantioRepository _repository = repository;

        public async Task<IEnumerable<PlantioDTO>> GetAllAsync()
    {
        var plantios = await _repository.GetAllAsync();
        return plantios.Select(p => new PlantioDTO
        {
            Id = p.Id,
            DataPlantio = p.DataPlantio,
            HortalicaId = p.HortalicaId,
            FuncionarioId = p.FuncionarioId,
            Quantidade = p.Quantidade
        });
    }

    public async Task<PlantioDTO> GetByIdAsync(int id)
    {
        var plantio = await _repository.GetByIdAsync(id);
        return new PlantioDTO
        {
            Id = plantio.Id,
            DataPlantio = plantio.DataPlantio,
            HortalicaId = plantio.HortalicaId,
            FuncionarioId = plantio.FuncionarioId,
            Quantidade = plantio.Quantidade
        };
    }

    public async Task CreateAsync(PlantioDTO plantioDto)
    {
        var plantio = new Plantio
        {
            DataPlantio = plantioDto.DataPlantio,
            HortalicaId = plantioDto.HortalicaId,
            FuncionarioId = plantioDto.FuncionarioId,
            Quantidade = plantioDto.Quantidade
        };
        await _repository.AddAsync(plantio);
    }

    public async Task UpdateAsync(PlantioDTO plantioDto)
    {
        var plantio = new Plantio
        {
            Id = plantioDto.Id,
            DataPlantio = plantioDto.DataPlantio,
            HortalicaId = plantioDto.HortalicaId,
            FuncionarioId = plantioDto.FuncionarioId,
            Quantidade = plantioDto.Quantidade
        };
        await _repository.UpdateAsync(plantio);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}

}