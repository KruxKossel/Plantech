using AutoMapper;
using Plantech.DTOs;
using Plantech.Interfaces;
using Plantech.Models;

namespace Plantech.Services;

public class LotesInsumosService(ILotesInsumosRepository lotesInsumosRepository, IMapper mapper): ILotesInsumosService
{
    private readonly ILotesInsumosRepository _lotesInsumosRepository = lotesInsumosRepository;
    private readonly IMapper _mapper = mapper;

    public async Task EditarLote(LotesInsumoDTO lotesInsumoDTO)
    {
        var lotesInsumo = _mapper.Map<LotesInsumo>(lotesInsumoDTO);
        await _lotesInsumosRepository.EditarLote(lotesInsumo);
    }

    public async Task<LotesInsumoDTO> GetLotesInsumoId(int id)
    {
        var lotes = await _lotesInsumosRepository.GetLotesInsumoId(id);
        return   _mapper.Map<LotesInsumoDTO>(lotes);
    }

    public async Task<List<LotesInsumoDTO>> ListarLotes()
    {
        var lotes = await _lotesInsumosRepository.ListarLotes();
        return _mapper.Map<List<LotesInsumoDTO>>(lotes);
    }

}
