using Plantech.Models;

namespace Plantech.Interfaces;

public interface IVendasRepository
{
    Task <int> CriarVenda(Vendas venda);
    // Task AtualizarCompra(OrdensCompras ordensCompra);
    Task <List<Vendas>> ListarVendas();
    Task <Vendas> BuscarId(int id);
    // Task AtualizarStatus(int id);
    // Task <List<Fornecedores>> ListarFornecedoresAsync();
    // Task <Fornecedores> ObterFornecedorPorId(int id);
    // Task <Funcionarios> ObterFuncionarioPorId(int id);
    // Task AdicionarInsumo(IEnumerable<InsumosCompras> insumos);
    Task DeletarTuplasZeradas();
    // Task <OrdensCompras> GetOrdensCompraId(int id);
    Task <IEnumerable<HortalicasVendas>> DetalharVenda(int id);

    // Task DeletarCompra(int id);
}
