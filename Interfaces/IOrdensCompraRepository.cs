using Plantech.Models;

namespace Plantech.Interfaces;

public interface IOrdensCompraRepository
{
    Task<int> CriarCompra(OrdensCompras ordensCompra);
    Task AtualizarCompra(OrdensCompras ordensCompra);
    Task <List<OrdensCompras>> ListarCompras();
    Task <OrdensCompras> BuscarId(int id);
    Task AtualizarStatus(int id);
    Task <List<Fornecedores>> ListarFornecedoresAsync();
    Task <Fornecedores> ObterFornecedorPorId(int id);
    Task <Funcionarios> ObterFuncionarioPorId(int id);
    Task AdicionarInsumo(IEnumerable<InsumosCompras> insumos);
    Task DeletarTuplasZeradas();
    Task <OrdensCompras> GetOrdensCompraId(int id);
    Task <IEnumerable<InsumosCompras>> DetalharCompra(int id);

    Task DeletarCompra(int id);
}
