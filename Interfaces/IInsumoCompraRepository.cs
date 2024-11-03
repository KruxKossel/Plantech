using Plantech.Models;

namespace Plantech.Interfaces
{
    public interface IInsumoCompraRepository
    {
        Task <List<InsumosCompra>> ListarInsumosCompra();
        Task <InsumosCompra> GetInsumosCompraId(int id);
        Task RemoverInsumo(int id);
        Task AdcionarInsumo(Insumo insumo);
        Task AlterarQuantidade(int id,int quantidade);
    }
}
