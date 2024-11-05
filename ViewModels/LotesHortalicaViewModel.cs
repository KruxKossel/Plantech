using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plantech.ViewModels
{
    public class LotesHortalicaViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "ID da Hortaliça")]
        public int HortalicaId { get; set; }

        [Display(Name = "Quantidade")]
        public int? Quantidade { get; set; }

        [Display(Name = "Preço de Venda")]
        public double? PrecoVenda { get; set; }

        [Display(Name = "Data de Entrada")]
        public DateOnly? DataEntrada { get; set; }

        [Display(Name = "Data de Validade")]
        public DateOnly? DataValidade { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; } = null!;

        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = null!;

        [Display(Name = "Colheitas")]
        public List<ColheitaViewModel> Colheita { get; set; } = new List<ColheitaViewModel>();

        [Display(Name = "Hortaliça")]
        public HortalicaViewModel Hortalica { get; set; } = null!;
    }
}
