using System;
using System.Collections.Generic;

namespace Plantech.DTOs;

public partial class InsumosCompraDTO
{
    public int OrdemCompraId { get; set; }

    public int InsumoId { get; set; }

    public int? LoteId { get; set; }

    public int? Quantidade { get; set; }

    public double? PrecoUnitario { get; set; }

    public DateOnly? DataChegada { get; set; }
    public virtual InsumoDTO Insumo { get; set; } = null!;

}
