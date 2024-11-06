using System.Threading.Tasks;
using System.Collections.Generic;
using Plantech.DTOs;
using Plantech.Models;

namespace Plantech.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> AuthenticateAsync(string username, string password);
        Task<UsuarioDTO> GetByIdAsync(int id);
        Task<IEnumerable<UsuarioDTO>> GetAllAsync();
        Task CreateAsync(UsuarioDTO userDto);
        Task UpdateAsync(UsuarioDTO userDto);
        Task DeleteAsync(int id);
        Task<Cargos> GetCargoByUserIdAsync(int userId);
        Task<UsuarioDTO> GetByEmailAsync(string email);
        Task<FuncionarioDTO> GetFuncionarioByUserIdAsync(int userId);
    }
}
