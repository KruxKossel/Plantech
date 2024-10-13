using System.Collections.Generic;
using System.Threading.Tasks;
using Plantech.Models;

namespace Plantech.Interfaces
{
    public interface IHortalicaRepository
    {
        Task AdicionarAsync(Hortalica hortalica);
        Task AtualizarAsync(Hortalica hortalica);
        Task DeletarAsync(int id);
        Task<Hortalica> ObterPorIdAsync(int id);
        Task<List<Hortalica>> ListarAsync();
    }
}
