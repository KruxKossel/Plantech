using System.Collections.Generic;
using System.Threading.Tasks;
using Plantech.Models;

namespace Plantech.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuarios> GetByIdAsync(int id);
        Task<Usuarios> GetByUsernameAsync(string username);
        Task<IEnumerable<Usuarios>> GetAllAsync();
        Task AddAsync(Usuarios user);
        Task UpdateAsync(Usuarios user);
        Task DeleteAsync(int id);
        Task<Usuarios> GetByEmailAsync(string email);

        Task<Usuarios> GetUltimoUsuarioAsync();
    }
}
