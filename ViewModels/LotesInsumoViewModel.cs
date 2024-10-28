using System;
using System.ComponentModel.DataAnnotations;

namespace Plantech.ViewModels
{
    public class LotesInsumoViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Insumo")]
        [Required(ErrorMessage = "O campo Insumo é obrigatório.")]
        public int InsumoId { get; set; }

        [Display(Name = "Quantidade")]
        public int? Quantidade { get; set; }

        [Display(Name = "Preço Unitário")]
        [DataType(DataType.Currency)]
        public double? PrecoUnitario { get; set; }

        [Display(Name = "Data de Entrada")]
        [DataType(DataType.Date)]
        public DateOnly? DataEntrada { get; set; }

        [Display(Name = "Data de Validade")]
        [DataType(DataType.Date)]
        public DateOnly? DataValidade { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "O campo Status é obrigatório.")]
        public string Status { get; set; } = null!;

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; } = null!;
    }
}
