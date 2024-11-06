using Plantech.DTOs;
using Plantech.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plantech.Interfaces
{
    public interface IInsumoRepository
    {
        Task CreateAsync(Insumos insumo);
        Task AtualizarAsync(Insumos insumo);
        Task DeletarAsync(int id);
        Task<Insumos> ObterPorIdAsync(int id);
        Task<List<Insumos>> ListarAsync();
        Task<InsumoDTO> ObterPorCnpjAsync(string cnpj);
        Task<List<Fornecedores>> ListarFornecedoresAsync();
    }
}
