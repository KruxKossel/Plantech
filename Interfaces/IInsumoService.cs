using Plantech.DTOs;

namespace Plantech.Interfaces
{
    public interface IInsumoService
    {
        Task CreateAsync(InsumoDTO insumo);
        Task AtualizarAsync(InsumoDTO insumo);
        Task DeletarAsync(int id);
        Task<InsumoDTO> ObterPorIdAsync(int id);
        Task<List<InsumoDTO>> ListarAsync();
        Task<InsumoDTO> ObterPorCnpjAsync(string cnpj);
        Task<List<FornecedoreDTO>> ListarFornecedoresAsync();

    }
}
