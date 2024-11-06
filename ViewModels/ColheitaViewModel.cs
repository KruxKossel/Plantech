using System;
using System.ComponentModel.DataAnnotations;

namespace Plantech.ViewModels
{
    public class ColheitaViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Quantidade")]
        public int? Quantidade { get; set; }

        [Display(Name = "Data da Colheita")]
        public DateOnly? DataColheita { get; set; }

        [Required]
        [Display(Name = "ID do Funcionário")]
        public int FuncionarioId { get; set; }

        [Display(Name = "ID do Lote de Hortaliça")]
        public int? LoteHortalicaId { get; set; }

        [Display(Name = "ID do Lote de Insumo")]
        public int? LoteInsumoId { get; set; }

        [Required]
        [Display(Name = "ID do Plantio")]
        public int PlantioId { get; set; }


        // [Display(Name = "Funcionário")]
        // public FuncionarioViewModel Funcionario { get; set; }


        

        [Display(Name = "Lote de Hortaliça")]
        public LotesHortalicaViewModel? LoteHortalica { get; set; }

        [Display(Name = "Lote de Insumo")]
        public LotesInsumoViewModel? LoteInsumo { get; set; }

        // [Display(Name = "Plantio")]
        // public PlantioViewModel Plantio { get; set; }
    }
}
