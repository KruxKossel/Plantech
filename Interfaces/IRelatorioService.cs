using Plantech.DTOs;
using Plantech.Models;
using Plantech.ViewModels;

namespace Plantech.Interfaces;

public interface IRelatorioService
{
    Task <RelatorioDTO> GerarDados();
    Task<RelatorioViewModel> ProcessandoDados();
    // Task <int> CriarVenda(VendaDTO vendaDTO);
    // Task <List<VendaDTO>> ListarVendas();
    // Task <VendaDTO> BuscarId(int id);
    // Task DeletarTuplasZeradas();
    // Task AdicionarHortalica(List<HortalicasVendaDTO> hortalicas);
    // Task <IEnumerable<HortalicasVendaDTO>> DetalharVenda(int id);
}
