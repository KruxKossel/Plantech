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
            if (funcionario == null)
            {
                return null; // ou lance uma exceção, se apropriado
            }
            return _mapper.Map<FuncionarioDTO>(funcionario); // mapear o objeto completo
        }

        public async Task<CargoDTO> GetCargoByIdAsync(int id)
        {
            var cargo = await _funcionarioRepository.GetCargoByIdAsync(id);
            if (cargo == null)
            {
                return null; // ou lance uma exceção, se apropriado
            }
            return _mapper.Map<CargoDTO>(cargo); // mapear o objeto completo
        }




        public async Task UpdateAsync(FuncionarioDTO funcionarioDto)
        {
            var funcionario = _mapper.Map<Funcionarios>(funcionarioDto);
            await _funcionarioRepository.UpdateAsync(funcionario);
        }


        public async Task<IEnumerable<FuncionarioDTO>> GetFuncionariosAsync()
        {
            var funcionario = await _funcionarioRepository.GetFuncionariosAsync();
            return _mapper.Map<IEnumerable<FuncionarioDTO>>(funcionario);
        }

        // public async Task<IEnumerable<Funcionarios>> ObterFuncionariosPorIdsAsync(List<int> ids)
        // {
        //     var funcionario = await _funcionarioRepository.ObterFuncionariosPorIdsAsync(ids);
        //     return _mapper.Map<FuncionarioDTO>(funcionario.Id);
        // }

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