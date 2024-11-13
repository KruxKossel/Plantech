using Plantech.DTOs;
using Plantech.Models;

namespace Plantech.Interfaces;

public interface IRelatorioRepository
{
    Task<RelatorioDTO> GerarDados();
    // Task <int> CriarVenda(Vendas venda);
    // Task <List<Vendas>> ListarVendas();
    // Task <Vendas> BuscarId(int id);
    // Task AdicionarHortalica(List<HortalicasVendas> hortalicas);
    // Task <IEnumerable<HortalicasVendas>> DetalharVenda(int id);
}
