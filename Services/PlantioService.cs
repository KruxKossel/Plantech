using AutoMapper;
using Plantech.DTOs;
using Plantech.Interfaces;
using Plantech.Models;

public class PlantioService(IMapper mapper, IPlantioRepository plantioRepository) : IPlantioService
{
    private readonly IMapper _mapper = mapper;
    private readonly IPlantioRepository _plantioRepository = plantioRepository;

    public async Task<PlantioDTO> GetUltimoPlantioAsync()
    {
        var plantioId = await _plantioRepository.GetUltimoPlantioAsync();
        return _mapper.Map<PlantioDTO>(plantioId);
    }

    public async Task<IEnumerable<PlantioDTO>> GetAllAsync()
    {
        var plantios = await _plantioRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PlantioDTO>>(plantios);
    }

    public async Task<PlantioDTO> GetByIdAsync(int id)
    {
        var plantio = await _plantioRepository.GetByIdAsync(id);
        return _mapper.Map<PlantioDTO>(plantio);
    }

    public async Task CreatePlantioAsync(PlantioDTO plantioDto)
    {
        var plantio = _mapper.Map<Plantio>(plantioDto);
        await _plantioRepository.CreatePlantioAsync(plantio);
    }

    public async Task CreateInsumosPlantioAsync(InsumosPlantioDTO insumoPlantioDto)
    {
        var insumoPlantio = _mapper.Map<InsumosPlantio>(insumoPlantioDto);
        await _plantioRepository.CreateInsumosPlantioAsync(insumoPlantio);
    }

    public async Task UpdateAsync(PlantioDTO plantioDto)
    {
        var plantio = _mapper.Map<Plantio>(plantioDto);
        await _plantioRepository.UpdateAsync(plantio);
    }

    public async Task DeleteAsync(int id)
    {
        await _plantioRepository.DeleteAsync(id);
    }

    // Métodos para obter listas de funcionários e hortaliças
     public async Task<IEnumerable<FuncionarioDTO>> GetFuncionariosAsync()
    {

        var funcionario = await _plantioRepository.GetFuncionariosAsync();
        return _mapper.Map<IEnumerable<FuncionarioDTO>>(funcionario);
    }

    public async Task<IEnumerable<HortalicaDTO>> GetHortalicasAsync()
    {
        var hortalicas = await _plantioRepository.GetHortalicasAsync();
        return _mapper.Map<IEnumerable<HortalicaDTO>>(hortalicas);
    }

    public async Task<IEnumerable<LotesInsumoDTO>> GetLotesInsumosAsync(){

         var lotesInsumo = await _plantioRepository.GetLotesInsumosAsync();
        return _mapper.Map<IEnumerable<LotesInsumoDTO>>(lotesInsumo);

    }

}
