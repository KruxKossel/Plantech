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

        public async Task<Funcionario> GetByUserIdAsync(int userId)
        {
            return await _context.Funcionarios
                .Include(f => f.Cargo)
                .SingleOrDefaultAsync(f => f.UsuarioId == userId);
        }

    }
}