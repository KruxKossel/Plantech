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
        Task AdicionarAsync(Fornecedores fornecedor);
        Task AtualizarDadosAsync(Fornecedores fornecedore);
        Task<List<Fornecedores>> ListarAsync();
        Task AtualizarStatusAsync(int id);
        Task<Fornecedores> ObterPorIdAsync(int id);
        Task<List<Fornecedores>> BuscarPorRazaoSocialAsync(string razaoSocial);
    }
}