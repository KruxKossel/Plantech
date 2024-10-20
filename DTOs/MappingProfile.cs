namespace Plantech;
using AutoMapper;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Plantech.DTOs;
using Plantech.Models;
using Plantech.ViewModels;

public class MappingProfile: Profile
{
    public MappingProfile(){
        CreateMap<HortalicaDTO, Hortalica>().ReverseMap();
        CreateMap<HortalicaViewModel, HortalicaDTO>().ReverseMap();
        CreateMap<InsumoDTO, Insumo>().ReverseMap();
        CreateMap<ClienteDTO, Cliente>().ReverseMap();
        CreateMap<FornecedoreDTO, Fornecedore>().ReverseMap();
        CreateMap<FornecedoreViewModel, FornecedoreDTO>().ReverseMap();
    }
}
