using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Plantech.Data;
using Plantech.DTOs;
using Plantech.Interfaces;
using Plantech.Models;

namespace Plantech.Repositories;

public class InsumoCompraService(IInsumoCompraRepository insumoCompraRepository,IMapper mapper): IInsumoCompraService
{
    private readonly IInsumoCompraRepository _insumoCompraRepository = insumoCompraRepository;
    private readonly IMapper _mapper = mapper;

    public Task AdcionarInsumo(Insumo insumo)
    {
        throw new NotImplementedException();
    }

    public Task AlterarQuantidade(int id, int quantidade)
    {
        throw new NotImplementedException();
    }

    public async Task<InsumosCompraDTO> GetInsumosCompraId(int id)
    {
        
        var insumo = await _insumoCompraRepository.GetInsumosCompraId(id);
        return  _mapper.Map<InsumosCompraDTO>(insumo);
    }

    public async Task<List<InsumosCompraDTO>> ListarInsumosCompra()
    {
        var insumoscompras = await _insumoCompraRepository.ListarInsumosCompra();
        return _mapper.Map<List<InsumosCompraDTO>>(insumoscompras);
    }

    public Task RemoverInsumo(int id)
    {
        throw new NotImplementedException();
    }
}
