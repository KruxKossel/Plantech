using Plantech.DTOs;
using Plantech.Models;

namespace Plantech.Interfaces
{
    public interface IInsumoCompraService
    {
        Task <List<InsumosCompraDTO>> ListarInsumosCompra();
        Task <InsumosCompraDTO> GetInsumosCompraId(int id);
        Task RemoverInsumo(int id);
        Task AdcionarInsumo(Insumos insumo);
        Task AlterarQuantidade(int id,int quantidade);
    }
}
