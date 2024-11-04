using Plantech.Data;
using Plantech.Models;
using Microsoft.EntityFrameworkCore;
using Plantech.Interfaces;
using AutoMapper;
using Plantech.DTOs;

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

    public Task AtualizarCompra(OrdensCompra ordensCompra)
    {
        throw new NotImplementedException();
    }

    public async Task<OrdensCompra> BuscarId(int id)
    {
        return await _context.OrdensCompras.FindAsync(id);
    }

public async Task<int> CriarCompra(OrdensCompra ordensCompra)
{
    // if (ordensCompra.FornecedorId <= 0) 
    // {
    //     throw new ArgumentException("FornecedorId não pode ser nulo ou inválido.");
    // }

    // // Tente encontrar o fornecedor e verificar se já está sendo rastreado
    // var fornecedor = await _context.Fornecedores.FindAsync(ordensCompra.FornecedorId);
    // if (fornecedor == null)
    // {
    //     throw new ArgumentException("Fornecedor não encontrado.");
    // }

    // // Se o fornecedor já está sendo rastreado, use a instância existente
    // if (_context.Entry(fornecedor).State == EntityState.Detached)
    // {
    //     _context.Attach(fornecedor); // Anexa a entidade existente
    // }

    // try
    // {
        await _context.OrdensCompras.AddAsync(ordensCompra);
        await _context.SaveChangesAsync();
    // }
    return ordensCompra.Id;
    // catch (DbUpdateException ex)
    // {
    //     throw new Exception("Erro ao salvar a compra. Verifique os dados e tente novamente.", ex);
    // }
}


    public async Task<List<OrdensCompra>> ListarCompras()
    {
        // var Compras = await _context.OrdensCompras.Include(o => o.Fornecedor).Include(o => o.Funcionario).ToListAsync();
        return await _context.OrdensCompras.Include(o => o.Fornecedor).Include(o => o.Funcionario).ToListAsync();
    }

    public async Task<List<Fornecedore>> ListarFornecedoresAsync()
    {
        return await _context.Fornecedores.ToListAsync();
    }

    public async Task<Fornecedore> ObterFornecedorPorId(int id)
    {
        var fornecedor =  await _context.Fornecedores
        .Where(f => f.Id == id)
        .Select(f => new Fornecedore 
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

    public async Task<Funcionario> ObterFuncionarioPorId(int id)
    {
                var funcionario =  await _context.Funcionarios
        .Where(f => f.Id == id)
        .Select(f => new Funcionario
        {
            Id = f.Id,
        })
        .FirstOrDefaultAsync();

        if(funcionario  != null){
            return funcionario;
        }
        return null;
    }

    public async Task AdicionarInsumo(IEnumerable<InsumosCompra> insumos)
    {
        foreach (var insumo in insumos)
        {
            await _context.InsumosCompras.AddAsync(insumo);
        }
        await _context.SaveChangesAsync();
    }
}
