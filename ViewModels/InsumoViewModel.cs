using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Plantech.ViewModels;

public partial class InsumoViewModel
{
    public int Id { get; set; }

    [DisplayName("Fornecedor")]
    [Display(Name = "Fornecedor")]
    public int? FornecedorId { get; set; } = null!;

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [Display(Name = "Nome")]
    public string Nome { get; set; } = null!;

    [Display(Name = "Descrição")]
    public string? Descricao { get; set; }

    [Display(Name = "Observações")]
    public string? Observacoes { get; set; }

    [Display(Name = "Categoria")]
    public string? Categoria { get; set; }

    [Required(ErrorMessage = "O tipo é obrigatório.")]
    [Display(Name = "Tipo")]
    public string Tipo { get; set; } = null!;

    [Display(Name = "Caminho da Imagem")]
    public string? CaminhoImagem { get; set; }

    [Display(Name = "Imagem")]
    public IFormFile? ImagemArquivo { get; set; }
}
