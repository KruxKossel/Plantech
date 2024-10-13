using System.Collections.Generic;
using System.Threading.Tasks;
using Plantech.Models;

namespace Plantech.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetByIdAsync(int id);
        Task<Usuario> GetByUsernameAsync(string username);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task AddAsync(Usuario user);
        Task UpdateAsync(Usuario user);
        Task DeleteAsync(int id);
    }
}
