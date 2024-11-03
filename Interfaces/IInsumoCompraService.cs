using Plantech.DTOs;
using Plantech.Models;

namespace Plantech.Interfaces
{
    public interface IInsumoCompraService
    {
        Task <List<InsumosCompraDTO>> ListarInsumosCompra();
        Task <InsumosCompraDTO> GetInsumosCompraId(int id);
        Task RemoverInsumo(int id);
        Task AdcionarInsumo(Insumo insumo);
        Task AlterarQuantidade(int id,int quantidade);
    }
}
