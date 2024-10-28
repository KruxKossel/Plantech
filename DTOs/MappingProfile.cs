namespace Plantech;
using AutoMapper;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Plantech.DTOs;
using Plantech.Models;
using Plantech.ViewModels;

public class MappingProfile: Profile
{
    public MappingProfile(){
        //Hortalica
        CreateMap<HortalicaDTO, Hortalica>().ReverseMap();
        CreateMap<HortalicaViewModel, HortalicaDTO>().ReverseMap();
        //Insumo
        CreateMap<InsumoDTO, Insumo>().ReverseMap();
        CreateMap<InsumoViewModel, InsumoDTO>().ReverseMap();
        //Cliente
        CreateMap<ClienteDTO, Cliente>().ReverseMap();
        CreateMap<ClienteViewModel, ClienteDTO>().ReverseMap();
        //Fornecedor
        CreateMap<FornecedoreDTO, Fornecedore>().ReverseMap();
        CreateMap<FornecedoreViewModel, FornecedoreDTO>().ReverseMap();

        //Funcionario
        CreateMap<FuncionarioDTO, Funcionario>().ReverseMap();


        //Plantio
        CreateMap<Plantio, PlantioDTO>().ReverseMap();
        CreateMap<PlantioViewModel, PlantioDTO>().ReverseMap(); 
    
        CreateMap<InsumosPlantio, InsumosPlantioDTO>().ReverseMap();

        //Lotes Insumo
        CreateMap<LotesInsumo, LotesInsumoDTO>().ReverseMap();
        CreateMap<LotesInsumoDTO, LotesInsumoViewModel>().ReverseMap();       



    }
}
