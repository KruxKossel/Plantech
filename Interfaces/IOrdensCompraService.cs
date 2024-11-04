using Plantech.DTOs;
using Plantech.Models;

namespace Plantech.Interfaces;

public interface IOrdensCompraService
{
    Task<int> CriarCompra(OrdensCompraDTO ordensCompradto);
    Task AtualizarCompra(OrdensCompraDTO ordensCompradto);
    Task <List<OrdensCompraDTO>> ListarCompras();
    Task <OrdensCompraDTO> BuscarId(int id);
    Task AtualizarStatus(int id);
    Task <List<FornecedoreDTO>> ListarFornecedoresAsync();
    Task <FornecedoreDTO> ObterFornecedorPorId(int id);
    Task <FuncionarioDTO> ObterFuncionarioPorId(int id);
    Task AdicionarInsumo(IEnumerable<InsumosCompraDTO> insumodto);

}
