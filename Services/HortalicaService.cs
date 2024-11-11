using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Plantech.DTOs;
using Plantech.Interfaces;
using Plantech.Models;

namespace Plantech.Services
{
    public class HortalicaService(IHortalicaRepository hortalicaRepository, IMapper mapper) : IHortalicaService
    {
        private readonly IHortalicaRepository _hortalicaRepository = hortalicaRepository;
        private readonly IMapper _mapper = mapper;

        public async Task CreateHortalicaAsync(HortalicaDTO hortalicaDto)
        {
            var hortalica = _mapper.Map<Hortalicas>(hortalicaDto);
            await _hortalicaRepository.AdicionarAsync(hortalica);
        }

        public async Task AtualizarHortalicaAsync(HortalicaDTO hortalicaDto)
        {
            var hortalica = _mapper.Map<Hortalicas>(hortalicaDto);
            await _hortalicaRepository.AtualizarAsync(hortalica);
        }

        public async Task DeletarHortalicaAsync(int id)
        {
            await _hortalicaRepository.DeletarAsync(id);
        }

        public async Task<HortalicaDTO?> ObterHortalicaPorIdAsync(int id)
        {
            var hortalica = await _hortalicaRepository.ObterPorIdAsync(id);
            return _mapper.Map<HortalicaDTO>(hortalica);
        }

        // public async Task<IEnumerable<Hortalicas>> ObterHortalicasPorIdsAsync(List<int> ids){

        //     var hortalica = await _hortalicaRepository.ObterHortalicasPorIdsAsync(ids);
        //     return _mapper.Map<HortalicaDTO>(hortalica.Id);
        // }

        public async Task<List<HortalicaDTO>> ListarHortalicasAsync()
        {
            var hortalicas = await _hortalicaRepository.ListarAsync();
            return _mapper.Map<List<HortalicaDTO>>(hortalicas);
        }

        public async Task AtualizarStatusAsync(int id)
        {
            await _hortalicaRepository.AtualizarStatusAsync(id);
        }
    }
}
