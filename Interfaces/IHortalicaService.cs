using Plantech.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plantech.Interfaces
{
    public interface IHortalicaService
    {
        Task CreateHortalicaAsync(HortalicaDTO hortalicaDto);
        Task AtualizarHortalicaAsync(HortalicaDTO hortalicaDto);
        Task DeletarHortalicaAsync(int id);
        Task<HortalicaDTO> ObterHortalicaPorIdAsync(int id);
        Task<List<HortalicaDTO>> ListarHortalicasAsync();
        Task AtualizarStatusAsync(int id);
    }
}
