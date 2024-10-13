using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plantech.Enuns;
using Plantech.Models;
using Plantech.Repositories;

namespace Plantech.Interfaces
{
    public interface IFornecedorService
    {
        Task AdicionarAsync(Fornecedore fornecedor);
        Task AtualizarDadosAsync(Fornecedore fornecedore);
        Task<List<Fornecedore>> ListarAsync();
        Task<Fornecedore> ObterPorIdAsync(int id);
        Task AtualizarStatusAsync(int id, Fornecedore fornecedore);
        Task<List<Fornecedore>> BuscarPorRazaoSocialAsync(string razaoSocial);
    }
}