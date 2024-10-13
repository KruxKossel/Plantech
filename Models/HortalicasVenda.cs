using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class HortalicasVenda
{
    public int VendaId { get; set; }

    public int LoteId { get; set; }

    public int? Quantidade { get; set; }

    public double? PrecoUnitario { get; set; }

    public virtual LotesHortalica Lote { get; set; } = null!;

    public virtual Venda Venda { get; set; } = null!;
}
