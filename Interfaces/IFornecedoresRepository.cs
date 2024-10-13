using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Plantech.Enuns;
using Plantech.Models;

namespace Plantech.Interfaces
{
    public interface IFornecedoresRepository
    {
        Task AdicionarAsync(Fornecedore fornecedor);
        Task AtualizarDadosAsync(Fornecedore fornecedore);
        Task<List<Fornecedore>> ListarAsync();
        Task AtualizarStatusAsync(int id, StatusPessoa statusPessoa);

        Task<Fornecedore> ObterPorIdAsync(int id);
        Task<List<Fornecedore>> BuscarPorRazaoSocialAsync(string razaoSocial);
    }

}