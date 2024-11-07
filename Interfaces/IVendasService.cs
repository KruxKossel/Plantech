using Plantech.DTOs;
using Plantech.Models;

namespace Plantech.Interfaces;

public interface IVendasService
{
    Task <int> CriarVenda(VendaDTO vendaDTO);
    // Task AtualizarCompra(OrdensCompras ordensCompra);
    Task <List<VendaDTO>> ListarVendas();
    Task <VendaDTO> BuscarId(int id);
    // Task AtualizarStatus(int id);
    // Task <List<Fornecedores>> ListarFornecedoresAsync();
    // Task <Fornecedores> ObterFornecedorPorId(int id);
    // Task <Funcionarios> ObterFuncionarioPorId(int id);
    // Task AdicionarInsumo(IEnumerable<InsumosCompras> insumos);
    Task DeletarTuplasZeradas();
    // Task <OrdensCompras> GetOrdensCompraId(int id);
    // Task <IEnumerable<HortalicasVendaDTO>> DetalharVenda(int id);

    // Task DeletarCompra(int id);
}
