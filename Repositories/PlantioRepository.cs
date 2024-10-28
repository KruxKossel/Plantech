using Microsoft.EntityFrameworkCore;
using Plantech.Data;
using Plantech.Interfaces;
using Plantech.Models;

public class PlantioRepository(PlantechContext context) : IPlantioRepository
{
    private readonly PlantechContext _context = context;
    

    public async Task<IEnumerable<Plantio>> GetAllAsync()
    {
        return await _context.Plantios.ToListAsync();
    }

    public async Task<Plantio> GetByIdAsync(int id)
    {
        return await _context.Plantios.FindAsync(id);
    }

    public async Task UpdateAsync(Plantio plantio)
    {
        if (PlantioExists(plantio.Id))
        {
            _context.Plantios.Update(plantio);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        if (PlantioExists(id))
        {
            var plantio = await _context.Plantios.FindAsync(id);
            if (plantio != null)
            {
                _context.Plantios.Remove(plantio);
                await _context.SaveChangesAsync();
            }
        }
    }

    public async Task AddPlantioWithInsumosAsync(Plantio plantio, List<InsumosPlantio> insumosPlantios)
    {
        await _context.Plantios.AddAsync(plantio);
        await _context.SaveChangesAsync(); // Salva e gera o ID do Plantio

        foreach (var insumo in insumosPlantios)
        {
            var lote = await _context.LotesInsumos.FindAsync(insumo.LoteId);

            if (lote != null && insumo.Quantidade <= lote.Quantidade)
            {
                insumo.PlantioId = plantio.Id; // Associa o insumo ao plantio criado
                lote.Quantidade -= insumo.Quantidade; // Atualiza a quantidade no lote
                await _context.InsumosPlantios.AddAsync(insumo);
            }
            else
            {
                throw new Exception("Quantidade de insumo usada é maior que a quantidade disponível no lote.");
            }
        }

        await _context.SaveChangesAsync(); // Salva todos os insumos no banco de dados
    }

    private bool PlantioExists(int id)
    {
        return _context.Plantios.Any(e => e.Id == id);
    }

    // Métodos para obter listas de funcionários e hortaliças
     public async Task<IEnumerable<Funcionario>> GetFuncionariosAsync()
    {
        return await _context.Funcionarios.ToListAsync();
    }

    public async Task<IEnumerable<Hortalica>> GetHortalicasAsync()
    {
        return await _context.Hortalicas.ToListAsync();
    }

    // Lotes de insumo
    public async Task<IEnumerable<LotesInsumo>> GetLotesInsumosAsync()
    {
        return await _context.LotesInsumos.ToListAsync();
    }

}
