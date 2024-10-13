using System.Collections.Generic;
using System.Threading.Tasks;
using Plantech.Enuns;
using Plantech.Models;

namespace Plantech.Interfaces
{
    public interface IClientesRepository
    {
        Task Adicionar(Cliente cliente);
        Task AtualizarDadosAsync(Cliente cliente);
        List<Cliente> Listar();
        Task AtualizarStatusAsync(int id, StatusPessoa statusPessoa);
        List<Cliente> BuscarPorRazaoSocial(string razaoSocial);
    }
}

