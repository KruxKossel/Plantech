using AutoMapper;
using Plantech.DTOs;
using Plantech.Models;
using Plantech.ViewModels;

namespace Plantech.MappingProfiles
{
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
            
            // Cliente
            CreateMap<ClienteDTO, Clientes>().ReverseMap();
            CreateMap<ClienteViewModel, ClienteDTO>().ReverseMap();
            
            // Fornecedor
            CreateMap<FornecedoreDTO, Fornecedores>().ReverseMap();
            CreateMap<FornecedoreViewModel, FornecedoreDTO>().ReverseMap();

            // Funcionario
            CreateMap<Funcionarios, FuncionarioDTO>()
                .ForMember(dest => dest.OrdensCompras, opt => opt.MapFrom(src => src.OrdensCompras))
                .ReverseMap();

            CreateMap<FuncionarioDTO, FuncionarioViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ReverseMap();

            CreateMap<FuncionarioCreateViewModel, FuncionarioDTO>()
                .ForMember(dest => dest.CargoId, opt => opt.MapFrom(src => src.CargoId))
                .ReverseMap();

            CreateMap<FuncionarioCreateViewModel, UsuarioDTO>()
                .ForMember(dest => dest.NomeUsuario, opt => opt.MapFrom(src => src.NomeUsuario))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Senha, opt => opt.MapFrom(src => src.Senha))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap();

            // Usuario
            CreateMap<Usuarios, UsuarioDTO>().ReverseMap();
            
            CreateMap<UsuarioDTO, FuncionarioViewModel>()
                .ForMember(dest => dest.NomeUsuario, opt => opt.MapFrom(src => src.NomeUsuario))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Senha, opt => opt.MapFrom(src => src.Senha))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap();

            // Cargos
            CreateMap<Cargos, CargoDTO>().ReverseMap();
            
            CreateMap<CargoDTO, FuncionarioViewModel>()
                .ForMember(dest => dest.Funcao, opt => opt.MapFrom(src => src.Funcao))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
                .ReverseMap();

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

            // Insumos Compra
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

            // Lotes Hortalicas
            CreateMap<LotesHortalicas, LotesHortalicaDTO>().ReverseMap();
            CreateMap<LotesHortalicaViewModel, LotesHortalicaDTO>().ReverseMap();

            CreateMap<LotesHortalicaPendenciaViewModel, LotesHortalicaDTO>().ReverseMap();


            
            // Colheitas
            CreateMap<Colheitas, ColheitaDTO>()
                .ForMember(dest => dest.LoteHortalica, opt => opt.MapFrom(src => src.LoteHortalica))
                .ForMember(dest => dest.LoteInsumo, opt => opt.MapFrom(src => src.LoteInsumo))
                .ForMember(dest => dest.Funcionario, opt => opt.MapFrom(src => src.Funcionario))
                .ForMember(dest => dest.Plantio, opt => opt.MapFrom(src => src.Plantio))
                .ReverseMap();

            CreateMap<ColheitaDTO, ColheitaViewModel>().ReverseMap();

            // Plantios
            CreateMap<Plantios, PlantioDTO>().ReverseMap();
            CreateMap<PlantioViewModel, PlantioDTO>().ReverseMap();
            // CreateMap<Plantios, PlantioDTO>()
            //     .ForMember(dest => dest.Hortalica, opt => opt.MapFrom(src => src.Hortalica))
            //     .ReverseMap();

            CreateMap<InsumosPlantios, InsumosPlantioDTO>().ReverseMap();


             //Vendas
        CreateMap<Vendas, VendaDTO>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data)) 
            .ForMember(dest => dest.TotalVendas, opt => opt.MapFrom(src => src.TotalVendas)) 
            .ForMember(dest => dest.QuantidadeProdutos, opt => opt.MapFrom(src => src.QuantidadeProdutos)) 
            .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId))
            .ForMember(dest => dest.FuncionarioId, opt => opt.MapFrom(src => src.FuncionarioId));

        CreateMap<VendaDTO, Vendas>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data)) 
            .ForMember(dest => dest.TotalVendas, opt => opt.MapFrom(src => src.TotalVendas))
            .ForMember(dest => dest.QuantidadeProdutos, opt => opt.MapFrom(src => src.QuantidadeProdutos))
            .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId))
            .ForMember(dest => dest.FuncionarioId, opt => opt.MapFrom(src => src.FuncionarioId));

         CreateMap<VendaDTO, VendaViewModel>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
            .ForMember(dest => dest.TotalVendas, opt => opt.MapFrom(src => src.TotalVendas))
            .ForMember(dest => dest.QuantidadeProdutos, opt => opt.MapFrom(src => src.QuantidadeProdutos))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId));


        CreateMap<VendaViewModel, VendaDTO>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
            .ForMember(dest => dest.TotalVendas, opt => opt.MapFrom(src => src.TotalVendas))
            .ForMember(dest => dest.QuantidadeProdutos, opt => opt.MapFrom(src => src.QuantidadeProdutos))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId));

            // CreateMap<(Vendas, List<LotesHortalica>), AdicionarHortalicaViewModel>()
            // .ForMember(dest => dest.Venda, opt => opt.MapFrom(src => src.Item1))
            // .ForMember(dest => dest.LotesDisponiveis, opt => opt.MapFrom(src => src.Item2));

            CreateMap<HortalicasVendaDTO, HortalicasVendas>();
            CreateMap<HortalicasVendaViewModel,HortalicaDTO>().ReverseMap();
            CreateMap<HortalicasVendas,HortalicasVendaDTO>();



            // Hortaliças perdidas e culturas perdidas

            CreateMap<HortalicasPerdidasDTO, HortalicasPerdidas>().ReverseMap();
            CreateMap<CulturasPerdidasDTO, CulturasPerdidas>().ReverseMap();

            CreateMap<CulturasPerdidasViewModel, HortalicasPerdidasDTO>().ReverseMap();
            CreateMap<CulturasPerdidasViewModel, CulturasPerdidasDTO>().ReverseMap();


        }
    }
}
