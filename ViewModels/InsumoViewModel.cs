using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Plantech.ViewModels;

public partial class InsumoViewModel
{
    public int Id { get; set; }

    [DisplayName("Cnpj Fornecedor")]
    public string  FornecedorId { get; set; } = null!; 

    public string Nome { get; set; } = null!;

    public string? Descricao { get; set; }

    public string? Observacoes { get; set; }

    public string? Categoria { get; set; }

    public string Tipo { get; set; } = null!;

    [Display(Name = "Imagem")]
    public IFormFile? ImagemArquivo { get; set; }
}
