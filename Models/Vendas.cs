using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class Vendas
{
    public int Id { get; set; }

    public DateOnly? Data { get; set; }

    public double? TotalVendas { get; set; }

    public int? QuantidadeProdutos { get; set; }

    public string Status { get; set; } = null!;

    public int ClienteId { get; set; }

    public int FuncionarioId { get; set; }

    public virtual Clientes Cliente { get; set; } = null!;

    public virtual Funcionarios Funcionario { get; set; } = null!;

    public virtual ICollection<HortalicasVendas> HortalicasVendas { get; set; } = new List<HortalicasVendas>();
}
