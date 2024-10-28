using Plantech.Models;

namespace Plantech.Interfaces;

public interface IOrdensCompraRepository
{
    Task CriarCompra(OrdensCompra ordensCompra);
    Task AtualizarCompra(OrdensCompra ordensCompra);
    Task <List<OrdensCompra>> ListarCompras();
    Task <OrdensCompra> BuscarId(int id);
    Task AtualizarStatus(int id);
    Task <List<Fornecedore>> ListarFornecedoresAsync();
    Task <Fornecedore> ObterFornecedorPorId(int id);
}
