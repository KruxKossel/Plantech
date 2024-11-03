﻿using Plantech.DTOs;
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
    
    public Task AtualizarStatus(int id)
    {
        throw new NotImplementedException();
    }

    public Task AtualizarCompra(OrdensCompraDTO ordensCompradto)
    {
        throw new NotImplementedException();
    }

    public async Task<OrdensCompraDTO> BuscarId(int id)
    {
        throw new NotImplementedException();
    }

    public async Task CriarCompra(OrdensCompraDTO ordensCompradto)
    {
       var ordensCompra = _mapper.Map<OrdensCompra>(ordensCompradto);
        await _ordensCompraRepository.CriarCompra(ordensCompra);
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
}