namespace Plantech.DTOs;
using AutoMapper;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
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
        // CreateMap<InsumoViewModel, InsumoViewModel>()
        // CreateMap<InsumoDTO, InsumoViewModel>().ReverseMap();
        // Cliente
        CreateMap<ClienteDTO, Cliente>().ReverseMap();
        CreateMap<ClienteViewModel, ClienteDTO>().ReverseMap();
        // Fornecedor
        CreateMap<FornecedoreDTO, Fornecedore>().ReverseMap();
        CreateMap<FornecedoreViewModel, FornecedoreDTO>().ReverseMap();

        // Funcionario
        CreateMap<Funcionario, FuncionarioDTO>()
            .ForMember(dest => dest.OrdensCompras, opt => opt.MapFrom(src => src.OrdensCompras)); 
        
        // Ordens Compra
        CreateMap<OrdensCompra, OrdensCompraDTO>()
            .ForMember(dest => dest.Fornecedor, opt => opt.MapFrom(src => src.Fornecedor)) 
            .ForMember(dest => dest.Funcionario, opt => opt.MapFrom(src => src.Funcionario)); 

        CreateMap<OrdensCompraDTO, OrdensCompra>()
            .ForMember(dest => dest.Fornecedor, opt => opt.MapFrom(src => src.Fornecedor))
            .ForMember(dest => dest.Funcionario, opt => opt.MapFrom(src => src.Funcionario));

        CreateMap<OrdensCompraViewModel, OrdensCompraDTO>().ReverseMap();
        // LotesInsumos
        CreateMap<LotesInsumo, LotesInsumoDTO>().ReverseMap();
        CreateMap<LotesInsumoDTO, LotesInsumoViewModel>().ReverseMap();

        //Insumos Compra
             CreateMap<InsumoDTO, InsumosCompraDTO>()
            .ForMember(dest => dest.InsumoId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Quantidade, opt => opt.Ignore()); 


        CreateMap<InsumosCompraDTO, InsumoDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InsumoId))
            .ForMember(dest => dest.Nome, opt => opt.Ignore()); 
        
            CreateMap<OrdensCompra, OrdensCompraDTO>()
                .ForMember(dest => dest.InsumosCompras, opt => opt.MapFrom(src => src.InsumosCompras));
            
            CreateMap<InsumosCompra, InsumosCompraDTO>()
                .ForMember(dest => dest.InsumoId, opt => opt.MapFrom(src => src.InsumoId))
                .ForMember(dest => dest.Quantidade, opt => opt.MapFrom(src => src.Quantidade));
            
            CreateMap<InsumosCompraDTO, InsumosCompra>()
                .ForMember(dest => dest.InsumoId, opt => opt.MapFrom(src => src.InsumoId))
                .ForMember(dest => dest.Quantidade, opt => opt.MapFrom(src => src.Quantidade));
    
            CreateMap<OrdensCompraViewModel, InsumosCompraDTO>()
                .ForMember(dest => dest.OrdemCompraId, opt => opt.MapFrom(src => src.Id)) 
                .ForMember(dest => dest.InsumoId, opt => opt.Ignore())
                .ForMember(dest => dest.Quantidade, opt => opt.Ignore()) 
                .ForMember(dest => dest.PrecoUnitario, opt => opt.Ignore());



        //Lotes Hortalicas
        CreateMap<LotesHortalica,LotesHortalicaDTO>().ReverseMap();
        CreateMap<LotesHortalicaViewModel, LotesHortalicaDTO>().ReverseMap();

        //Colheita
               CreateMap<Colheita, ColheitaDTO>()
            .ForMember(dest => dest.LoteHortalica, opt => opt.MapFrom(src => src.LoteHortalica))
            .ForMember(dest => dest.LoteInsumo, opt => opt.MapFrom(src => src.LoteInsumo))
            .ForMember(dest => dest.Funcionario, opt => opt.MapFrom(src => src.Funcionario))
            .ForMember(dest => dest.Plantio, opt => opt.MapFrom(src => src.Plantio))
            .ReverseMap();
        CreateMap<ColheitaDTO, ColheitaViewModel>().ReverseMap();

        //Plantio
        CreateMap<Plantio, PlantioDTO>().ReverseMap();
        CreateMap<PlantioViewModel, PlantioDTO>().ReverseMap(); 
        CreateMap<Plantio, PlantioDTO>()
            .ForMember(dest => dest.Hortalica, opt => opt.MapFrom(src => src.Hortalica))
            .ReverseMap();
        CreateMap<InsumosPlantio, InsumosPlantioDTO>().ReverseMap();
    
    }
}
