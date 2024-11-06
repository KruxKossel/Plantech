using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Plantech.Data;
using Plantech.DTOs;
using Plantech.Interfaces;
using Plantech.Models;


namespace Plantech.Services;

public class LotesHortalicasService(ILotesHortalicasRepository lotesHortalicasRepository, IMapper mapper): ILotesHortalicasService
{
    private readonly ILotesHortalicasRepository _lotesHortalicasRepository = lotesHortalicasRepository;
    private readonly IMapper _mapper = mapper;

    public async Task EditarLote(LotesHortalicaDTO lotesHortalicaDto)
    {
        var lotesHortalica = _mapper.Map<LotesHortalicas>(lotesHortalicaDto);
        await _lotesHortalicasRepository.EditarLote(lotesHortalica);
    }

    public async Task<LotesHortalicaDTO> GetLotesInsumoId(int id)
    {
        var lotesHortalica = await _lotesHortalicasRepository.GetLotesInsumoId(id);
        return _mapper.Map<LotesHortalicaDTO>(lotesHortalica);
    }

    public async Task<List<LotesHortalicaDTO>> ListarLotes()
    {
        var lotesHortalicas = await _lotesHortalicasRepository.ListarLotes();
        return _mapper.Map<List<LotesHortalicaDTO>>(lotesHortalicas);
    }


}
