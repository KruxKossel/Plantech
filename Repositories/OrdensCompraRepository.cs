using Plantech.Data;
using Plantech.Models;
using Microsoft.EntityFrameworkCore;
using Plantech.Interfaces;
using AutoMapper;
using Plantech.DTOs;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace Plantech.Repositories;

public class OrdensCompraRepository : IOrdensCompraRepository
{
    private readonly PlantechContext _context;
    private readonly IMapper _mapper;
    public OrdensCompraRepository(PlantechContext plantechContext, IMapper mapper){
        _context = plantechContext;
        _mapper = mapper;
    }
    public Task AtualizarStatus(int id)
    {
        throw new NotImplementedException();
    }

    public Task AtualizarCompra(OrdensCompras ordensCompra)
    {
        throw new NotImplementedException();
    }

    public async Task<OrdensCompras> BuscarId(int id)
    {
        return await _context.OrdensCompras.FindAsync(id);
    }

public async Task<int> CriarCompra(OrdensCompras ordensCompra)
{
        await _context.OrdensCompras.AddAsync(ordensCompra);
        await _context.SaveChangesAsync();
  
    return ordensCompra.Id; 
}


    public async Task<List<OrdensCompras>> ListarCompras()
    {
        return await _context.OrdensCompras.Include(o => o.Fornecedor).Include(o => o.Funcionario).ToListAsync();
    }

    public async Task<List<Fornecedores>> ListarFornecedoresAsync()
    {
        return await _context.Fornecedores.ToListAsync();
    }

    public async Task<Fornecedores> ObterFornecedorPorId(int id)
    {
        var fornecedor =  await _context.Fornecedores
        .Where(f => f.Id == id)
        .Select(f => new Fornecedores 
        {
            Id = f.Id,
            Cnpj = f.Cnpj,
        })
        .FirstOrDefaultAsync();

        if(fornecedor != null){
            return fornecedor;
        }
        return null;
    }

    public async Task<Funcionarios> ObterFuncionarioPorId(int id)
    {
                var funcionario =  await _context.Funcionarios
        .Where(f => f.Id == id)
        .Select(f => new Funcionarios
        {
            Id = f.Id,
        })
        .FirstOrDefaultAsync();

        if(funcionario  != null){
            return funcionario;
        }
        return null;
    }

    public async Task AdicionarInsumo(IEnumerable<InsumosCompras> insumos)
    {
        foreach (var insumo in insumos)
        {
            if (insumo.InsumoId <= 0)
            {
                throw new ArgumentException("InsumoId deve ser maior que zero.");
            }
            var existente = await _context.Insumos.FindAsync(insumo.InsumoId);
            if (existente == null)
            {
                throw new ArgumentException($"Insumo com ID {insumo.InsumoId} não encontrado.");
            }

            await _context.InsumosCompras.AddAsync(insumo);
        }
        await _context.SaveChangesAsync();
    }

        public async Task DeletarTuplasZeradas()
        {
            var tuplasPraDeletar = await _context.OrdensCompras
                .Where(o => o.Total == 0 || o.Total == null)
                .ToListAsync();

            if (tuplasPraDeletar.Any())
            {
                _context.OrdensCompras.RemoveRange(tuplasPraDeletar);
                await _context.SaveChangesAsync();
            }
        }

    public async Task<OrdensCompras> GetOrdensCompraId(int id)
    {
        return await _context.OrdensCompras.Include(o => o.Funcionario).Include(o=>o.Fornecedor).FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<InsumosCompras>> DetalharCompra(int id)
    {
        var ordem = await GetOrdensCompraId(id);
        if (ordem == null)
        {
            throw new ArgumentException($"Ordem de compra com ID {id} não encontrada.");
        }

        return await _context.InsumosCompras.Include(i=>i.Insumo)
            .Where(insumo => insumo.OrdemCompraId == id) 
            .ToListAsync();
    }

    public async Task DeletarCompra(int id)
    {
        var compra = await _context.OrdensCompras.FindAsync(id);
        var insumos = await _context.InsumosCompras.Where(i => i.OrdemCompraId == compra.Id).ToListAsync();
        if(compra.Status == "pendente"){
        foreach(var insumo in insumos){
         _context.InsumosCompras.Remove(insumo);
        await _context.SaveChangesAsync();
        }
        _context.OrdensCompras.Remove(compra);
        await _context.SaveChangesAsync();
        }
        else{
            throw new ArgumentException($"A compra ja foi paga!");
        }
    }
}
