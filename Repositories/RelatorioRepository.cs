using Plantech.Data;
using Plantech.Models;
using Microsoft.EntityFrameworkCore;
using Plantech.Interfaces;
using AutoMapper;
using Plantech.DTOs;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace Plantech.Repositories;

public class RelatorioRepository : IRelatorioRepository
{
    private readonly PlantechContext _context;
    private readonly IMapper _mapper;
    
    public RelatorioRepository(PlantechContext plantechContext, IMapper mapper){
        _context = plantechContext;
        _mapper = mapper;
    }

    public async Task<RelatorioDTO> GerarDados()
    {
        var compras = await _context.OrdensCompras.ToListAsync();
        var vendas = await _context.Vendas.ToListAsync();
        var plantios = await _context.Plantios.Where(p=> p.Status == "não colhida").ToListAsync();
        var colheitas = await _context.Colheitas.ToListAsync();
        var comprasdto = _mapper.Map<List<OrdensCompraDTO>>(compras);
        var vendasdto = _mapper.Map<List<VendaDTO>>(vendas);
        var plantiosdto =_mapper.Map<List<PlantioDTO>>(plantios);
        var colheitasdto = _mapper.Map<List<ColheitaDTO>>(colheitas);
        
        var relatoriodto = new RelatorioDTO{
            Compras = comprasdto,
            Venda = vendasdto,
            Plantio = plantiosdto,
            Colheita = colheitasdto
        };

        return relatoriodto;
    }
}
