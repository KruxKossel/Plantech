using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Plantech.ViewModels
{
    public class PlantioViewModel
    {
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
    }
}