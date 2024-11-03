namespace Plantech.DTOs;
using AutoMapper;
using Plantech.Models;
using Plantech.ViewModels;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Hortalica
        CreateMap<HortalicaDTO, Hortalica>().ReverseMap();
        CreateMap<HortalicaViewModel, HortalicaDTO>().ReverseMap();
        
        // Insumo
        CreateMap<InsumoDTO, Insumo>().ReverseMap();
        CreateMap<InsumoViewModel, InsumoDTO>().ReverseMap();
        
        // Cliente
        CreateMap<ClienteDTO, Cliente>().ReverseMap();
        CreateMap<ClienteViewModel, ClienteDTO>().ReverseMap();
        
        // Fornecedor
        CreateMap<FornecedoreDTO, Fornecedore>().ReverseMap();
        CreateMap<FornecedoreViewModel, FornecedoreDTO>().ReverseMap();

        // Funcionario
        CreateMap<Funcionario, FuncionarioDTO>()
            .ForMember(dest => dest.OrdensCompras, opt => opt.MapFrom(src => src.OrdensCompras)); // Verifique se OrdensCompras existe em FuncionarioDTO
        
        // Ordens Compra
        CreateMap<OrdensCompra, OrdensCompraDTO>()
            .ForMember(dest => dest.Fornecedor, opt => opt.MapFrom(src => src.Fornecedor)) // Mapeia o fornecedor
            .ForMember(dest => dest.Funcionario, opt => opt.MapFrom(src => src.Funcionario)); // Mapeia o funcionário

        CreateMap<OrdensCompraDTO, OrdensCompra>()
            .ForMember(dest => dest.Fornecedor, opt => opt.MapFrom(src => src.Fornecedor))
            .ForMember(dest => dest.Funcionario, opt => opt.MapFrom(src => src.Funcionario));

        CreateMap<OrdensCompraViewModel, OrdensCompraDTO>().ReverseMap();

        // LotesInsumos
        CreateMap<LotesInsumo,LotesInsumoDTO>().ReverseMap();
        CreateMap<LotesInsumoViewModel, LotesInsumoDTO>().ReverseMap();
        //

    }
}
