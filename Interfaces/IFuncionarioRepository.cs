using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plantech.Models;

namespace Plantech.Interfaces
{
    public interface IFuncionarioRepository
    {
        Task<Funcionario> GetByUserIdAsync(int userId);

        Task<IEnumerable<Funcionario>> GetFuncionariosAsync();
    }
}
