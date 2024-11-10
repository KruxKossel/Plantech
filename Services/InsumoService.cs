using AutoMapper;
using Plantech.Data;
using Plantech.DTOs;
using Plantech.Interfaces;
using Plantech.Models;

namespace Plantech.Services;

public class InsumoService(IInsumoRepository insumoRepository, IMapper mapper): IInsumoService
{
    private readonly IInsumoRepository _insumoRepository = insumoRepository;
    private readonly IMapper _mapper = mapper;

    public async Task AtualizarAsync(InsumoDTO insumoDto)
    {
        var insumo = _mapper.Map<Insumos>(insumoDto);
        await _insumoRepository.AtualizarAsync(insumo);
    }

    public async Task DeletarAsync(int id)
    {
        await _insumoRepository.DeletarAsync(id);
    }

    public async Task CreateAsync(InsumoDTO insumoDto)
    {
        var insumo = _mapper.Map<Insumos>(insumoDto);
        await _insumoRepository.CreateAsync(insumo);
    }

    public async Task<List<InsumoDTO>> ListarAsync()
    {
        var insumos = await _insumoRepository.ListarAsync();
        return _mapper.Map<List<InsumoDTO>>(insumos);
    }

    public async Task<InsumoDTO> ObterPorIdAsync(int id)
    {
        var insumo = await _insumoRepository.ObterPorIdAsync(id);
        return _mapper.Map<InsumoDTO>(insumo);
    }

        public async Task<InsumoDTO> ObterPorCnpjAsync(string cnpj)
        {
            return await _insumoRepository.ObterPorCnpjAsync(cnpj);
        }

    public async Task<List<FornecedoreDTO>> ListarFornecedoresAsync()
    {
        var fornecedores = await _insumoRepository.ListarFornecedoresAsync();
        return _mapper.Map<List<FornecedoreDTO>>(fornecedores);
    }

    public async Task AtualizarStatusAsync(int id)
    {
        await _insumoRepository.AtualizarStatusAsync(id);
    }
}
