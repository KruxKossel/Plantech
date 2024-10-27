using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plantech.DTOs;
using Plantech.Enuns;
using Plantech.Models;
using Plantech.Repositories;

namespace Plantech.Interfaces
{
    public interface IFornecedorService
    {
        Task AdicionarAsync(FornecedoreDTO fornecedor);
        Task AtualizarDadosAsync(FornecedoreDTO fornecedor);
        Task<List<FornecedoreDTO>> ListarAsync();
        Task<FornecedoreDTO> ObterPorIdAsync(int id);
        Task AtualizarStatusAsync(int id);
        Task<List<FornecedoreDTO>> BuscarPorRazaoSocialAsync(string razaoSocial);
    }
}