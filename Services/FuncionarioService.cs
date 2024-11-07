using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Plantech.DTOs;
using Plantech.Interfaces;
using Plantech.Models;

namespace Plantech.Services
{
    public class FuncionarioService(IFuncionarioRepository funcionarioRepository, IMapper mapper) : IFuncionarioService
    {

        private readonly IFuncionarioRepository _funcionarioRepository = funcionarioRepository;

        private readonly IMapper _mapper = mapper;
        public async Task<FuncionarioDTO> GetByIdAsync(int id)
        {
            var funcionario = await _funcionarioRepository.GetByIdAsync(id);
            return _mapper.Map<FuncionarioDTO>(funcionario.Id);
        }

        public async Task<IEnumerable<FuncionarioDTO>> GetFuncionariosAsync()
        {
            var funcionario = await _funcionarioRepository.GetFuncionariosAsync();
            return _mapper.Map<IEnumerable<FuncionarioDTO>>(funcionario);
        }

        public async Task CreateFuncionarioAsync(FuncionarioDTO funcionariosDto){
            
            var funcionarios = _mapper.Map<Funcionarios>(funcionariosDto);
            await _funcionarioRepository.CreateFuncionarioAsync(funcionarios);
        }

        public async Task<IEnumerable<CargoDTO>> GetCargosAsync()
        { 
            var cargos = await _funcionarioRepository.GetCargosAsync();
            return _mapper.Map<IEnumerable<CargoDTO>>(cargos);
        }

    }
}