using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class InsumosCompra
{
    public int OrdemCompraId { get; set; }

    public int InsumoId { get; set; }

    public int? LoteId { get; set; } 

    public int? Quantidade { get; set; }

    public double? PrecoUnitario { get; set; }

    public DateOnly? DataChegada { get; set; }

    public virtual Insumo Insumo { get; set; } = null!;

    public virtual LotesInsumo Lote { get; set; } = null!;

    public virtual OrdensCompra OrdemCompra { get; set; } = null!;
}
