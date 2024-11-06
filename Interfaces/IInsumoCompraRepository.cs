using Plantech.Models;

namespace Plantech.Interfaces
{
    public interface IInsumoCompraRepository
    {
        Task <List<InsumosCompras>> ListarInsumosCompra();
        Task <InsumosCompras> GetInsumosCompraId(int id);
        Task RemoverInsumo(int id);
        Task AdcionarInsumo(Insumos insumo);
        Task AlterarQuantidade(int id,int quantidade);
    }
}
