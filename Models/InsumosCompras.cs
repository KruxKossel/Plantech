using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class InsumosCompras
{
    public int OrdemCompraId { get; set; }

    public int InsumoId { get; set; }

    public int? LoteId { get; set; }

    public int? Quantidade { get; set; }

    public double? PrecoUnitario { get; set; }

    public string? DataChegada { get; set; }

    public int? Ponteiro { get; set; }

    public virtual Insumos Insumo { get; set; } = null!;

    public virtual LotesInsumos? Lote { get; set; }

    public virtual OrdensCompras OrdemCompra { get; set; } = null!;
}
