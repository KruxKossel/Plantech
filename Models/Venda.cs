using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class Venda
{
    public int Id { get; set; }

    public DateOnly? Data { get; set; }

    public double? TotalVendas { get; set; }

    public int? QuantidadeProdutos { get; set; }

    public string? Pagamento { get; set; }

    public int ClienteId { get; set; }

    public int FuncionarioId { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Funcionario Funcionario { get; set; } = null!;

    public virtual ICollection<HortalicasVenda> HortalicasVenda { get; set; } = new List<HortalicasVenda>();
}
