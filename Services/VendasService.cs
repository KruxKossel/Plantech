using Plantech.Data;
using Plantech.Models;
using Microsoft.EntityFrameworkCore;
using Plantech.Interfaces;
using AutoMapper;
using Plantech.DTOs;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace Plantech.Services;

public class VendasService : IVendasService
{
    private readonly IVendasRepository _vendasRepository;
    private readonly IMapper _mapper;
    public VendasService(IVendasRepository vendasRepository, IMapper mapper){
        _vendasRepository = vendasRepository;
        _mapper = mapper;
    }

    public async Task<VendaDTO> BuscarId(int id)
    {
        var venda = await _vendasRepository.BuscarId(id);
        return _mapper.Map<VendaDTO>(venda);
    }

    public async  Task <int> CriarVenda(VendaDTO vendaDTO)
    {
        var venda = _mapper.Map<Vendas>(vendaDTO);
        await _vendasRepository.CriarVenda(venda);
        return venda.Id;
    }

    public Task DeletarTuplasZeradas()
    {
        throw new NotImplementedException();
    }

    public async Task<List<VendaDTO>> ListarVendas()
    {
        var vendas = await _vendasRepository.ListarVendas();
        return _mapper.Map<List<VendaDTO>>(vendas);
    }
}
