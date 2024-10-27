using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Plantech.DTOs;
using Plantech.Interfaces;
using Plantech.Models;
using Plantech.Repositories;

namespace Plantech.Services
{
    public class FornecedoreService(IFornecedoresRepository fornecedoresRepository, IMapper mapper) : IFornecedorService
    {
        private readonly IFornecedoresRepository _fornecedoresRepository = fornecedoresRepository;
        private readonly IMapper _mapper = mapper;

        public async Task AdicionarAsync(FornecedoreDTO fornecedoreDTO)
        {
            var fornecedor = _mapper.Map<Fornecedore>(fornecedoreDTO);
            await _fornecedoresRepository.AdicionarAsync(fornecedor);
        }

        public async Task AtualizarDadosAsync(FornecedoreDTO fornecedoreDTO)
        {
            var fornecedor = _mapper.Map<Fornecedore>(fornecedoreDTO);
            await _fornecedoresRepository.AtualizarDadosAsync(fornecedor);
        }

        public async Task AtualizarStatusAsync(int id)
        {
            await _fornecedoresRepository.AtualizarStatusAsync(id);
        }

        public async Task<FornecedoreDTO?> ObterPorIdAsync(int id)
        {
            var fornecedor = await _fornecedoresRepository.ObterPorIdAsync(id);
            return _mapper.Map<FornecedoreDTO>(fornecedor);
        }

        public async Task<List<FornecedoreDTO>> ListarAsync()
        {
            var fornecedores = await _fornecedoresRepository.ListarAsync();
            return _mapper.Map<List<FornecedoreDTO>>(fornecedores);
        }

        public async Task<List<FornecedoreDTO?>> BuscarPorRazaoSocialAsync(string razaoSocial)
        {
            var fornecedor = await _fornecedoresRepository.BuscarPorRazaoSocialAsync(razaoSocial);
            return _mapper.Map<List<FornecedoreDTO>>(fornecedor);
        }

    }
}
