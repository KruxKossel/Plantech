using Plantech.Data;
using Plantech.Models;
using Microsoft.EntityFrameworkCore;
using Plantech.Interfaces;
using AutoMapper;
using Plantech.DTOs;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace Plantech.Repositories;

public class VendasRepository : IVendasRepository
{
    private readonly PlantechContext _context;
    private readonly IMapper _mapper;
    
    public VendasRepository(PlantechContext plantechContext, IMapper mapper){
        _context = plantechContext;
        _mapper = mapper;
    }

    public async Task AdicionarHortalica(List<HortalicasVendas> hortalicas)
    {
             foreach(var hortalica in hortalicas){
            if(hortalica.LoteId <= 0){
                throw new ArgumentException("Hortalica deve ser maior que zero.");
            }
            // var existe = await _context.Hortalicas.FindAsync(hortalica.LoteId);
            // if(existe == null){
            //     throw new ArgumentException($"Hortalica com ID {hortalica.LoteId} não encontrado.");
            // }
            await _context.HortalicasVendas.AddAsync(hortalica);
        }
        await _context.SaveChangesAsync();
    }

    public async Task<Vendas> BuscarId(int id)
    {
        return await _context.Vendas.Include(v => v.Cliente).Include(v => v.Funcionario).FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task <int> CriarVenda(Vendas venda)
    {
        await _context.Vendas.AddAsync(venda);
        await _context.SaveChangesAsync();
        return venda.Id;

    }

    public Task DeletarTuplasZeradas()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<HortalicasVendas>> DetalharVenda(int id)
    {
         throw new NotImplementedException();
    }

    public async Task<List<Vendas>> ListarVendas()
    {
        return await _context.Vendas.Include(v => v.Cliente).Include(v => v.Funcionario).ToListAsync();
    }
}
