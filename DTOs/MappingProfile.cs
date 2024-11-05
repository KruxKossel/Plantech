namespace Plantech;
using AutoMapper;
using Plantech.DTOs;
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
        CreateMap<FuncionarioDTO, Funcionario>().ReverseMap();
        CreateMap<FuncionarioViewModel, FuncionarioDTO>().ReverseMap();
        CreateMap<FuncionarioViewModel, UsuarioDTO>().ReverseMap();
        CreateMap<UsuarioDTO, Usuario>().ReverseMap();

        // Plantio
        CreateMap<Plantio, PlantioDTO>().ReverseMap();
        CreateMap<PlantioViewModel, PlantioDTO>().ReverseMap(); 
        CreateMap<Plantio, PlantioDTO>()
            .ForMember(dest => dest.Hortalica, opt => opt.MapFrom(src => src.Hortalica))
            .ReverseMap();
        CreateMap<InsumosPlantio, InsumosPlantioDTO>().ReverseMap();

        // Lotes Insumo
        CreateMap<LotesInsumo, LotesInsumoDTO>().ReverseMap();
        CreateMap<LotesInsumoDTO, LotesInsumoViewModel>().ReverseMap();

        // Colheita
        CreateMap<Colheita, ColheitaDTO>()
            .ForMember(dest => dest.LoteHortalica, opt => opt.MapFrom(src => src.LoteHortalica))
            .ForMember(dest => dest.LoteInsumo, opt => opt.MapFrom(src => src.LoteInsumo))
            .ForMember(dest => dest.Funcionario, opt => opt.MapFrom(src => src.Funcionario))
            .ForMember(dest => dest.Plantio, opt => opt.MapFrom(src => src.Plantio))
            .ReverseMap();
        CreateMap<ColheitaDTO, ColheitaViewModel>().ReverseMap();

        // Lotes Hortaliça
        CreateMap<LotesHortalica, LotesHortalicaDTO>().ReverseMap();
        CreateMap<LotesHortalicaDTO, LotesHortalicaViewModel>().ReverseMap();
    }
}
