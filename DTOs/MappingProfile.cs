namespace Plantech.DTOs;
using AutoMapper;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Plantech.Models;
using Plantech.ViewModels;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Hortalicas
        CreateMap<HortalicaDTO, Hortalicas>().ReverseMap();
        CreateMap<HortalicaViewModel, HortalicaDTO>().ReverseMap();
        
        // Insumos
        CreateMap<InsumoDTO, Insumos>().ReverseMap();
        CreateMap<InsumoViewModel, InsumoDTO>().ReverseMap();
        // CreateMap<InsumoViewModel, InsumoViewModel>()
        // CreateMap<InsumoDTO, InsumoViewModel>().ReverseMap();
        // Cliente
        CreateMap<ClienteDTO, Clientes>().ReverseMap();
        CreateMap<ClienteViewModel, ClienteDTO>().ReverseMap();
        // Fornecedor
        CreateMap<FornecedoreDTO, Fornecedores>().ReverseMap();
        CreateMap<FornecedoreViewModel, FornecedoreDTO>().ReverseMap();
        
        // CreateMap<string, DateOnly>()
        //     .ConvertUsing(str => DateOnly.ParseExact(str, "yyyy-MM-dd", null));

        // // Se necessário, configure o reverse mapping também
        // CreateMap<DateOnly, string>()
        //     .ConvertUsing(date => date.ToString("yyyy-MM-dd"));
   
        // Funcionario
        CreateMap<Funcionarios, FuncionarioDTO>()
            .ForMember(dest => dest.OrdensCompras, opt => opt.MapFrom(src => src.OrdensCompras)); 
        
        // Ordens Compra
        CreateMap<OrdensCompras, OrdensCompraDTO>()
            .ForMember(dest => dest.Fornecedor, opt => opt.MapFrom(src => src.Fornecedor)) 
            .ForMember(dest => dest.Funcionario, opt => opt.MapFrom(src => src.Funcionario)); 

        CreateMap<OrdensCompraDTO, OrdensCompras>()
            .ForMember(dest => dest.Fornecedor, opt => opt.MapFrom(src => src.Fornecedor))
            .ForMember(dest => dest.Funcionario, opt => opt.MapFrom(src => src.Funcionario));

        CreateMap<OrdensCompraViewModel, OrdensCompraDTO>().ReverseMap();
        // LotesInsumos
        CreateMap<LotesInsumos, LotesInsumoDTO>().ReverseMap();
        CreateMap<LotesInsumoDTO, LotesInsumoViewModel>().ReverseMap();

        //Insumos Compra
        CreateMap<InsumoDTO, InsumosCompraDTO>()
            .ForMember(dest => dest.InsumoId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Quantidade, opt => opt.Ignore()); 


        CreateMap<InsumosCompraDTO, InsumoDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InsumoId))
            .ForMember(dest => dest.Nome, opt => opt.Ignore()); 
        
        CreateMap<OrdensCompras, OrdensCompraDTO>()
            .ForMember(dest => dest.InsumosCompras, opt => opt.MapFrom(src => src.InsumosCompras));
            
        CreateMap<InsumosCompras, InsumosCompraDTO>()
            .ForMember(dest => dest.InsumoId, opt => opt.MapFrom(src => src.InsumoId))
            .ForMember(dest => dest.Quantidade, opt => opt.MapFrom(src => src.Quantidade));
            
        CreateMap<InsumosCompraDTO, InsumosCompras>()
            .ForMember(dest => dest.InsumoId, opt => opt.MapFrom(src => src.InsumoId))
            .ForMember(dest => dest.Quantidade, opt => opt.MapFrom(src => src.Quantidade));
    
        CreateMap<OrdensCompraViewModel, InsumosCompraDTO>()
            .ForMember(dest => dest.OrdemCompraId, opt => opt.MapFrom(src => src.Id)) 
            .ForMember(dest => dest.InsumoId, opt => opt.Ignore())
            .ForMember(dest => dest.Quantidade, opt => opt.Ignore()) 
            .ForMember(dest => dest.PrecoUnitario, opt => opt.Ignore());



        //Lotes Hortalicas
        CreateMap<LotesHortalicas,LotesHortalicaDTO>().ReverseMap();
        CreateMap<LotesHortalicaViewModel, LotesHortalicaDTO>().ReverseMap();

        //Colheitas
               CreateMap<Colheitas, ColheitaDTO>()
            .ForMember(dest => dest.LoteHortalica, opt => opt.MapFrom(src => src.LoteHortalica))
            .ForMember(dest => dest.LoteInsumo, opt => opt.MapFrom(src => src.LoteInsumo))
            .ForMember(dest => dest.Funcionario, opt => opt.MapFrom(src => src.Funcionario))
            .ForMember(dest => dest.Plantio, opt => opt.MapFrom(src => src.Plantio))
            .ReverseMap();
        CreateMap<ColheitaDTO, ColheitaViewModel>().ReverseMap();

        //Plantios
        CreateMap<Plantios, PlantioDTO>().ReverseMap();
        CreateMap<PlantioViewModel, PlantioDTO>().ReverseMap(); 
        CreateMap<Plantios, PlantioDTO>()
            .ForMember(dest => dest.Hortalica, opt => opt.MapFrom(src => src.Hortalica))
            .ReverseMap();
        CreateMap<InsumosPlantios, InsumosPlantioDTO>().ReverseMap();


        //Vendas
        CreateMap<Vendas, VendaDTO>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data)) 
            .ForMember(dest => dest.TotalVendas, opt => opt.MapFrom(src => src.TotalVendas)) 
            .ForMember(dest => dest.QuantidadeProdutos, opt => opt.MapFrom(src => src.QuantidadeProdutos)) 
            .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId))
            .ForMember(dest => dest.FuncionarioId, opt => opt.MapFrom(src => src.FuncionarioId));
            // .ForMember(dest => dest.HortalicasVendas, opt => opt.MapFrom(src => src.HortalicasVendas))
        CreateMap<VendaDTO, Vendas>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data)) 
            .ForMember(dest => dest.TotalVendas, opt => opt.MapFrom(src => src.TotalVendas))
            .ForMember(dest => dest.QuantidadeProdutos, opt => opt.MapFrom(src => src.QuantidadeProdutos))
            .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId))
            .ForMember(dest => dest.FuncionarioId, opt => opt.MapFrom(src => src.FuncionarioId));
            // .ForMember(dest => dest.HortalicasVendas, opt => opt.MapFrom(src => src.HortalicasVendas));
         CreateMap<VendaDTO, VendaViewModel>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
            .ForMember(dest => dest.TotalVendas, opt => opt.MapFrom(src => src.TotalVendas))
            .ForMember(dest => dest.QuantidadeProdutos, opt => opt.MapFrom(src => src.QuantidadeProdutos))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId));
            // .ForMember(dest => dest.FuncionarioId, opt => opt.MapFrom(src => src.FuncionarioId))
            // .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src.Cliente)) 
            // .ForMember(dest => dest.Funcionario, opt => opt.MapFrom(src => src.Funcionario)); 

        CreateMap<VendaViewModel, VendaDTO>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
            .ForMember(dest => dest.TotalVendas, opt => opt.MapFrom(src => src.TotalVendas))
            .ForMember(dest => dest.QuantidadeProdutos, opt => opt.MapFrom(src => src.QuantidadeProdutos))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId));
            // .ForMember(dest => dest.FuncionarioId, opt => opt.MapFrom(src => src.FuncionarioId))
            // .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src.Cliente))
            // .ForMember(dest => dest.Funcionario, opt => opt.MapFrom(src => src.Funcionario));

    }
}
