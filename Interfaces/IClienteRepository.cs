using System.Collections.Generic;
using System.Threading.Tasks;
using Plantech.Enuns;
using Plantech.Models;

namespace Plantech.Interfaces
{
    public interface IClienteRepository
    {
        Task Adicionar(Cliente cliente);
        Task AtualizarDadosAsync(Cliente cliente);
        Task <List<Cliente>> Listar();
        Task AtualizarStatusAsync(int id);
        Task <List<Cliente>> BuscarPorRazaoSocial(string razaoSocial);
        Task<Cliente> BuscarPorId(int id);
    }
}

