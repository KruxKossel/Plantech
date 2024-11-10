using System;
using System.Collections.Generic;

namespace Plantech.DTOs;

public partial class HortalicasVendaDTO
{
    public int VendaId { get; set; }

    public int LoteId { get; set; }

    public int? Quantidade { get; set; }

    public double? PrecoUnitario { get; set; }

    // public virtual LotesHortalicas Lote { get; set; } = null!;

    // public virtual Vendas Venda { get; set; } = null!;
}
