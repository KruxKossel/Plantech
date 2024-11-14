using Plantech.Data;
using Plantech.Models;
using Microsoft.EntityFrameworkCore;
using Plantech.Interfaces;
using AutoMapper;
using Plantech.DTOs;
using Plantech.ViewModels;

namespace Plantech.Services;

public class RelatorioService(IRelatorioRepository relatorioRepository, IMapper mapper) : IRelatorioService
{
    private readonly IRelatorioRepository _relatorioRepository = relatorioRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<RelatorioDTO> GerarDados()
    {
        return await _relatorioRepository.GerarDados();
    }

    public async Task<RelatorioViewModel> ProcessandoDados(){
        var relatorioDTO =await _relatorioRepository.GerarDados();
        double totalGastos = relatorioDTO.Compras.Sum(compra => compra?.Total ?? 0);
        double faturamento = relatorioDTO.Venda.Sum(compra => compra?.TotalVendas ?? 0);

        RelatorioViewModel relatorio = new RelatorioViewModel{
            TotalColheita = relatorioDTO.Colheita.Count(),
            TotalCompra = relatorioDTO.Compras.Count(),
            TotalPlantioAtivo = relatorioDTO.Plantio.Count(),
            TotalVenda = relatorioDTO.Venda.Count(),
            Gastos = totalGastos,
            Faturamento = faturamento
        };
        return relatorio;
    }







}
