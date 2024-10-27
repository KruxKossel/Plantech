using Plantech.DTOs;
using Plantech.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plantech.Interfaces
{
    public interface IInsumoRepository
    {
        Task CreateAsync(Insumo insumo);
        Task AtualizarAsync(Insumo insumo);
        Task DeletarAsync(int id);
        Task<Insumo> ObterPorIdAsync(int id);
        Task<List<Insumo>> ListarAsync();
        Task<InsumoDTO> ObterPorCnpjAsync(string cnpj);
        Task<List<Fornecedore>> ListarFornecedoresAsync();
    }
}
