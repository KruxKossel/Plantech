using System.Collections.Generic;
using System.Threading.Tasks;
using Plantech.Enuns;
using Plantech.Models;

namespace Plantech.Interfaces
{
    public interface IClienteRepository
    {
        Task Adicionar(Clientes cliente);
        Task AtualizarDadosAsync(Clientes cliente);
        Task <List<Clientes>> Listar();
        Task AtualizarStatusAsync(int id);
        Task <List<Clientes>> BuscarPorRazaoSocial(string razaoSocial);
        Task<Clientes> BuscarPorId(int id);
    }
}

