using System.Collections.Generic;
using System.Threading.Tasks;
using Plantech.Enuns;
using Plantech.Models;
using Plantech.DTOs;

namespace Plantech.Interfaces
{
    public interface IClienteService
    {
        Task AdicionarAsync(ClienteDTO clienteDTO);
        Task AtualizarDadosAsync(ClienteDTO cliente);
        Task<List<ClienteDTO>> ListarAsync();
        Task AtualizarStatusAsync(int id);
        Task<List<ClienteDTO>> BuscarPorRazaoSocial(string razaoSocial);
        Task<ClienteDTO> BuscarPorId(int id);
    }
}

