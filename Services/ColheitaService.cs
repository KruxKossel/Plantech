using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Plantech.DTOs;
using Plantech.Interfaces;
using Plantech.Models;

namespace Plantech.Services
{
    public class ColheitaService(IColheitaRepository colheitaRepository, IFuncionarioRepository funcionarioRepository,
                                    IPlantioRepository plantioRepository, IMapper mapper) : IColheitaService
    {

        private readonly IColheitaRepository _colheitaRepository = colheitaRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IPlantioRepository _plantioRepository = plantioRepository;

        private readonly IFuncionarioRepository _funcionarioRepository = funcionarioRepository;

            public async Task CreateColheitaAsync(ColheitaDTO colheitaDto)
            {
                var colheita = _mapper.Map<Colheitas>(colheitaDto);
                await _colheitaRepository.CreateColheitaAsync(colheita);
            }

            public async Task DeleteAsync(int id)
            {
                await _colheitaRepository.DeleteAsync(id);
            }

            public async Task<IEnumerable<ColheitaDTO>> GetAllAsync()
            {
                var colheitas = await _colheitaRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<ColheitaDTO>>(colheitas);
            }

            public async Task<ColheitaDTO> GetByIdAsync(int id)
            {
                var colheita = await _colheitaRepository.GetByIdAsync(id);
                return _mapper.Map<ColheitaDTO>(colheita);
            }

            public async Task<IEnumerable<FuncionarioDTO>> GetFuncionariosAsync()
            {
                var funcionario = await _funcionarioRepository.GetFuncionariosAsync();
                return _mapper.Map<IEnumerable<FuncionarioDTO>>(funcionario);
            }

            public async Task<IEnumerable<PlantioDTO>> GetPlantioAsync()
            {
                var plantios = await _plantioRepository.GetAllAsync();

                Console.WriteLine("\n\nPlantios recebidos:");
                foreach(var plantio in plantios)
                {
                    Console.WriteLine($"\n{plantio.Id}, {plantio.Status}, {plantio.Hortalica?.Nome}");
                }
                
                var plantiosDTO = _mapper.Map<IEnumerable<PlantioDTO>>(plantios);

                Console.WriteLine("\n\nPlantios mapeados para DTO:");
                foreach(var plantioDTO in plantiosDTO)
                {
                    Console.WriteLine($"\n{plantioDTO.Id}, {plantioDTO.Status}, {plantioDTO.Hortalica?.Nome}");
                }
                
                return plantiosDTO;
            }



            public async Task UpdateAsync(ColheitaDTO colheitaDto)
            {
                var colheita = _mapper.Map<Colheitas>(colheitaDto);
                await _colheitaRepository.UpdateAsync(colheita);
            }


            public async Task<IEnumerable<CulturasPerdidasDTO>> GetCulturasPerdidas()
            {
                var culturasPerdidas = await _colheitaRepository.GetCulturasPerdidas();
                var culturasPerdidasDto = _mapper.Map<IEnumerable<CulturasPerdidasDTO>>(culturasPerdidas);

                // Adicionar o nome da hortaliça e a data da colheita aos DTOs
                // foreach (var cultura in culturasPerdidasDto)
                // {
                //     var colheita = culturasPerdidas.First(c => c.Id == cultura.Id).Colheita;
                //     cultura.Colheita.DataColheita = colheita.DataColheita; // Supondo que há uma propriedade DataColheita em Colheita

                //     foreach (var hortalica in cultura.HortalicasPerdidas)
                //     {
                //         var hortalicaModel = culturasPerdidas.SelectMany(c => c.HortalicasPerdidas).First(h => h.CulturaPerdidaId == hortalica.CulturaPerdidaId && h.HortalicaId == h.HortalicaId);
                //         hortalica.Hortalica.Nome = hortalicaModel.Hortalica.Nome; // Supondo que há uma propriedade Nome em Hortalica
                //     }
                // }

                return culturasPerdidasDto;
    

            }

            public async Task<IEnumerable<HortalicasPerdidasDTO>> GetHortaPerdidas()
            {
                var perdidas = await _colheitaRepository.GetHortaPerdidas();
                return _mapper.Map<IEnumerable<HortalicasPerdidasDTO>>(perdidas);

            }
    }
}