using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class OrdensCompras
{
    public int Id { get; set; }

    public double? Total { get; set; }

    public string Status { get; set; } = null!;

    public int FuncionarioId { get; set; }

    public int FornecedorId { get; set; }

    public string? DataCompra { get; set; }

    public virtual Fornecedores Fornecedor { get; set; } = null!;

    public virtual Funcionarios Funcionario { get; set; } = null!;

    public virtual ICollection<InsumosCompras> InsumosCompras { get; set; } = new List<InsumosCompras>();
}
