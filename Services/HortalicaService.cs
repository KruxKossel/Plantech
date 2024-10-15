using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plantech.DTOs;
using Plantech.Interfaces;
using Plantech.Models;

namespace Plantech.Services
{
    public class HortalicaService(IHortalicaRepository hortalicaRepository) : IHortalicaService
    {
        private readonly IHortalicaRepository _hortalicaRepository = hortalicaRepository;

        public async Task CreateHortalicaAsync(HortalicaDTO hortalicaDto)
        {
            var hortalica = new Hortalica
            {
                Nome = hortalicaDto.Nome,
                Descricao = hortalicaDto.Descricao,
                Observacoes = hortalicaDto.Observacoes,
                CaminhoImagem = hortalicaDto.CaminhoImagem
            };

            await _hortalicaRepository.AdicionarAsync(hortalica);
        }

        public async Task AtualizarHortalicaAsync(HortalicaDTO hortalicaDto)
        {
            var hortalica = new Hortalica
            {
                Id = hortalicaDto.Id,
                Nome = hortalicaDto.Nome,
                Descricao = hortalicaDto.Descricao,
                Observacoes = hortalicaDto.Observacoes,
                CaminhoImagem = hortalicaDto.CaminhoImagem
            };

            await _hortalicaRepository.AtualizarAsync(hortalica);
        }

        public async Task DeletarHortalicaAsync(int id)
        {
            await _hortalicaRepository.DeletarAsync(id);
        }

        public async Task<HortalicaDTO?> ObterHortalicaPorIdAsync(int id)
        {
            var hortalica = await _hortalicaRepository.ObterPorIdAsync(id);
            return hortalica != null ? new HortalicaDTO
            {
                Id = hortalica.Id,
                Nome = hortalica.Nome,
                Descricao = hortalica.Descricao,
                Observacoes = hortalica.Observacoes,
                CaminhoImagem = hortalica.CaminhoImagem
            } : null;
        }

        public async Task<List<HortalicaDTO>> ListarHortalicasAsync()
        {
            var hortalicas = await _hortalicaRepository.ListarAsync();
            return hortalicas.Select(h => new HortalicaDTO
            {
                Id = h.Id,
                Nome = h.Nome,
                Descricao = h.Descricao,
                Observacoes = h.Observacoes,
                CaminhoImagem = h.CaminhoImagem
            }).ToList();
        }
    }
}
