using Plantech.DTOs;
using AutoMapper;
using Plantech.Repositories;
using Plantech.Interfaces;
using Plantech.Models;

namespace Plantech.Services;

public class OrdensCompraService : IOrdensCompraService
{
    private readonly IMapper _mapper;
    private readonly IOrdensCompraRepository _ordensCompraRepository;

    public OrdensCompraService(IOrdensCompraRepository ordensCompraRepository, IMapper mapper){
        _ordensCompraRepository = ordensCompraRepository;
        _mapper = mapper;
    }
    
    public async Task AtualizarStatus(int id)
    {
        await _ordensCompraRepository.AtualizarStatus(id);
    }

    public Task AtualizarCompra(OrdensCompraDTO ordensCompradto)
    {
        throw new NotImplementedException();
    }

    public async Task<OrdensCompraDTO> BuscarId(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CriarCompra(OrdensCompraDTO ordensCompradto)
    {
       var ordensCompra = _mapper.Map<OrdensCompras>(ordensCompradto);
        await _ordensCompraRepository.CriarCompra(ordensCompra);
        return ordensCompra.Id;
    }

    public async Task<List<OrdensCompraDTO>> ListarCompras()
    {
        var compras = await _ordensCompraRepository.ListarCompras();
        return _mapper.Map<List<OrdensCompraDTO>>(compras);
    }

    public async Task<List<FornecedoreDTO>> ListarFornecedoresAsync()
    {
        var fornecedores = await _ordensCompraRepository.ListarFornecedoresAsync();
        return _mapper.Map<List<FornecedoreDTO>>(fornecedores);
    }

    public async Task<FornecedoreDTO> ObterFornecedorPorId(int id)
    {
        var fornecedore = await _ordensCompraRepository.ObterFornecedorPorId(id);
        return _mapper.Map<FornecedoreDTO>(fornecedore);
    }

    public async Task<FuncionarioDTO> ObterFuncionarioPorId(int id)
    {
        var funcionario = await _ordensCompraRepository.ObterFuncionarioPorId(id);
        return _mapper.Map<FuncionarioDTO>(funcionario);
    }

    public async Task AdicionarInsumo(IEnumerable<InsumosCompraDTO> insumodto)
    {
        var insumo = _mapper.Map<IEnumerable<InsumosCompras>>(insumodto);
        await _ordensCompraRepository.AdicionarInsumo(insumo);
    }

    public async Task DeletarTuplasZeradas()
    {
        await _ordensCompraRepository.DeletarTuplasZeradas();
    }

    public async Task<OrdensCompraDTO> GetOrdensCompraId(int id)
    {
        var orden = await _ordensCompraRepository.GetOrdensCompraId(id);
        return _mapper.Map<OrdensCompraDTO>(orden);
    
    }

    public async Task<IEnumerable<InsumosCompraDTO>> DetalharCompra(int id)
    {
        var insumos = await _ordensCompraRepository.DetalharCompra(id);
        return _mapper.Map<IEnumerable<InsumosCompraDTO>>(insumos);
    }

    public async Task DeletarCompra(int id)
    {
        await _ordensCompraRepository.DeletarCompra(id);
    }
}
