using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plantech.Data;
using Plantech.Interfaces;
using Plantech.Models;

namespace Plantech.Repositories
{
    public class FuncionarioRepository(PlantechContext context) : IFuncionarioRepository
    {
        private readonly PlantechContext _context = context;

        public async Task<Funcionarios> GetByUserIdAsync(int userId)
        {
            return await _context.Funcionarios
                .Include(f => f.Cargo)
                .SingleOrDefaultAsync(f => f.UsuarioId == userId);
        }

        public async Task UpdateAsync(Funcionarios funcionario)
        {
            var existingEntity = await _context.Funcionarios.FindAsync(funcionario.Id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }

            _context.Entry(funcionario).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Adicione logging aqui para ajudar na depuração
                throw new Exception("Erro ao atualizar funcionário", ex);
            }
        }



        public async Task<Funcionarios> GetByIdAsync(int id)
        {
            return await _context.Funcionarios
                .Include(f => f.Cargo) // Inclui a entidade Cargo
                .Include(f => f.Usuario) // Inclui a entidade Usuario
                .FirstOrDefaultAsync(f => f.Id == id); // Busca a entidade Funcionario pelo ID
        }

        public async Task<Cargos> GetCargoByIdAsync(int id){

            return await _context.Cargos.FirstOrDefaultAsync(c => c.Id == id);
        }


         public async Task<IEnumerable<Funcionarios>> GetFuncionariosAsync()
        {
            return await _context.Funcionarios
            .Include(f => f.Cargo) // Inclui a entidade Cargo
            .Include(f => f.Usuario) // Inclui a entidade Usuario
            .ToListAsync();
        }

        public async Task CreateFuncionarioAsync(Funcionarios funcionarios){

            await _context.Funcionarios.AddAsync(funcionarios);
            await _context.SaveChangesAsync(); 
        }

        public async Task<IEnumerable<Cargos>> GetCargosAsync()
        {
            return await _context.Cargos.ToListAsync();
        }

        public async Task<IEnumerable<Funcionarios>> ObterFuncionariosPorIdsAsync(List<int> ids)
        {
            return await _context.Funcionarios
                .Where(f => ids.Contains(f.Id))
                .ToListAsync();
        }


    }
}