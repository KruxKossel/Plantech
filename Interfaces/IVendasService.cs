﻿using Plantech.DTOs;
using Plantech.Models;

namespace Plantech.Interfaces;

public interface IVendasService
{
    Task <int> CriarVenda(VendaDTO vendaDTO);
    Task <List<VendaDTO>> ListarVendas();
    Task <VendaDTO> BuscarId(int id);
    Task DeletarTuplasZeradas();
    Task AdicionarHortalica(List<HortalicasVendaDTO> hortalicas);
    Task <IEnumerable<HortalicasVendaDTO>> DetalharVenda(int id);
}
