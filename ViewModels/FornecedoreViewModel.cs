using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plantech.ViewModels;

public partial class FornecedoreViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "O CNPJ é obrigatório.")]
    [StringLength(18, MinimumLength = 14, ErrorMessage = "O CNPJ deve ter entre 14 e 18 caracteres.")]
    [RegularExpression(@"^(\d{14}|\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2})$", ErrorMessage = "O CNPJ deve ser apenas números ou no formato '00.000.000/0000-00'.")]
    public string Cnpj { get; set; } = null!;
    [Required(ErrorMessage = "A Razão Social é obrigatória.")]
    [StringLength(100, ErrorMessage = "A Razão Social não pode exceder 100 caracteres.")]
    public string RazaoSocial { get; set; } = null!;

    public string? Cidade { get; set; }

    public string? Endereco { get; set; }
    [EmailAddress(ErrorMessage = "Insira um email válido.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "O formato do email é inválido.")]
    [StringLength(100, ErrorMessage = "O email não pode exceder 100 caracteres.")]
    public string? Email { get; set; }

    public string Status { get; set; } = null!;

}
