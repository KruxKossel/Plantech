using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Plantech.DTOs;

namespace Plantech.ViewModels;

public partial class InsumoViewModel
{
    public int Id { get; set; }

    [DisplayName("Fornecedor")]
    public int?  FornecedorId { get; set; } = null!; 

    public string Nome { get; set; } = null!;

    public string? Descricao { get; set; }

    public string? Observacoes { get; set; }

    public string? Categoria { get; set; }

    public string Tipo { get; set; } = null!;
    
    public string Status { get; set; } = "ativo";

    public string? CaminhoImagem { get; set; }
    [Display(Name = "Imagem")]
    public IFormFile? ImagemArquivo { get; set; }
    [Display(Name = "Preço Unitário")]
    public double PrecoUnitario { get; set; } 

    public int QtdInsumos { get; set; } 

    public FornecedoreDTO? Fornecedor { get; set; }
}
