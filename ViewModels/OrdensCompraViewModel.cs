using System;
using System.Collections.Generic;
using Plantech.DTOs;

namespace Plantech.ViewModels;

public partial class OrdensCompraViewModel
{
    public int Id { get; set; }

    public double? Total { get; set; }

    public string Status { get; set; } = null!;

    public int FuncionarioId { get; set; }

    public int FornecedorId { get; set; }

    public DateOnly? DataCompra { get; set; }
    public virtual FornecedoreDTO Fornecedor { get; set; } = null!;
    public virtual FuncionarioDTO Funcionario { get; set; } = null!;
    // public int? Quantidade { get; set; }

    // public double? PrecoUnitario { get; set; }


}
