using System.Collections.Generic;
using System.Threading.Tasks;
using Plantech.Models;

namespace Plantech.Interfaces
{
    public interface IHortalicaRepository
    {
        Task AdicionarAsync(Hortalicas hortalica);
        Task AtualizarAsync(Hortalicas hortalica);
        Task DeletarAsync(int id);
        Task<Hortalicas> ObterPorIdAsync(int id);
        Task<List<Hortalicas>> ListarAsync();
        Task AtualizarStatusAsync(int id);
    }
}
