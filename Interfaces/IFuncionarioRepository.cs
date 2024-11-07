using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plantech.Models;

namespace Plantech.Interfaces
{
    public interface IFuncionarioRepository
    {
        Task<Funcionarios> GetByUserIdAsync(int userId);

        Task<Funcionarios> GetByIdAsync(int id);

        Task<IEnumerable<Funcionarios>> GetFuncionariosAsync();

        Task CreateFuncionarioAsync(Funcionarios funcionarios);

        Task<IEnumerable<Cargos>> GetCargosAsync();

    }
}
