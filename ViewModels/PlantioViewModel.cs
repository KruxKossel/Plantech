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
        public DateOnly? DataPlantio { get; set; }

        [Required(ErrorMessage = "Hortaliça é obrigatória")]
        public int HortalicaId { get; set; }

        [Required(ErrorMessage = "Funcionário é obrigatório")]
        public int FuncionarioId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantidade deve ser maior ou igual a 0")]
        public int? Quantidade { get; set; }

        public List<InsumosPlantioDTO> InsumosPlantios { get; set; } = new List<InsumosPlantioDTO>();


        // guarda nome do funcionario
        public string FuncionarioNome { get; set; }


        // guarda lotes insumo que serão listados
        public List<LotesInsumoDTO> LotesInsumos { get; set; } = new List<LotesInsumoDTO>();

    }
}
