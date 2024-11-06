using AutoMapper;
using Plantech.Data;
using Plantech.DTOs;
using Plantech.Interfaces;
using Plantech.Models;
using Plantech.Repositories;

namespace Plantech.Services;

public class ClienteService(IClienteRepository clienteRepository, IMapper mapper) : IClienteService
{
    private readonly IClienteRepository _clientesRepository = clienteRepository;
    private readonly IMapper _mapper = mapper;


    public async Task AdicionarAsync(ClienteDTO clienteDTO)
    {
        var cliente = _mapper.Map<Clientes>(clienteDTO);
        await _clientesRepository.Adicionar(cliente);
    }

    public async Task AtualizarDadosAsync(ClienteDTO clienteDTO)
    {
        var cliente = _mapper.Map<Clientes>(clienteDTO);
        await _clientesRepository.AtualizarDadosAsync(cliente);

    }

    public async Task AtualizarStatusAsync(int id)
    {
        await _clientesRepository.AtualizarStatusAsync(id);

    }

    public async Task<List<ClienteDTO>> BuscarPorRazaoSocial(string razaoSocial)
    {
        var cliente = await _clientesRepository.BuscarPorRazaoSocial(razaoSocial);
        return _mapper.Map<List<ClienteDTO>>(cliente);
    }

    public async Task<List<ClienteDTO>> ListarAsync()
    {
        var cliente = await _clientesRepository.Listar();
        return _mapper.Map<List<ClienteDTO>>(cliente);
    }

    public async Task<ClienteDTO>BuscarPorId(int id)
    {
        var cliente = await _clientesRepository.BuscarPorId(id);
        return _mapper.Map<ClienteDTO>(cliente);
    }
}