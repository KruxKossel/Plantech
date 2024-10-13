using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class OrdensCompra
{
    public int Id { get; set; }

    public double? Total { get; set; }

    public string Status { get; set; } = null!;

    public int FuncionarioId { get; set; }

    public int FornecedorId { get; set; }

    public DateOnly? DataCompra { get; set; }

    public virtual Fornecedore Fornecedor { get; set; } = null!;

    public virtual Funcionario Funcionario { get; set; } = null!;

    public virtual ICollection<InsumosCompra> InsumosCompras { get; set; } = new List<InsumosCompra>();
}
