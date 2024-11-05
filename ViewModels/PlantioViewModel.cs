using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Plantech.DTOs;

namespace Plantech.ViewModels
{
    public class PlantioViewModel
    {
        // Plantio
        public int Id { get; set; }

        [Required(ErrorMessage = "Data do plantio é obrigatória")]
        [DataType(DataType.Date)]
        [Display(Name = "Data do Plantio")]
        public DateOnly? DataPlantio { get; set; }

        [Required(ErrorMessage = "Hortaliça é obrigatória")]
        [Display(Name = "ID da Hortaliça")]
        public int HortalicaId { get; set; }

        [Required(ErrorMessage = "Funcionário é obrigatório")]
        [Display(Name = "ID do Funcionário")]
        public int FuncionarioId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantidade deve ser maior ou igual a 0")]
        [Display(Name = "Quantidade")]
        public int? Quantidade { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; } = null!;

        [Display(Name = "Insumos do Plantio")]
        public List<InsumosPlantioDTO> InsumosPlantios { get; set; } = new List<InsumosPlantioDTO>();

        [Display(Name = "Hortaliças")]
        public HortalicaDTO Hortalica { get; set; }


        // guarda lotes insumo que serão listados
        [Display(Name = "Lotes de Insumo")]
        public List<LotesInsumoDTO> LotesInsumos { get; set; } = new List<LotesInsumoDTO>();



        public string? HortalicaNome { get; set; }

        public string? FuncionarioNome { get; set; }

    }
}
