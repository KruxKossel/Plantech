using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class InsumosPlantios
{
    public int PlantioId { get; set; }

    public int LoteId { get; set; }

    public int? Quantidade { get; set; }

    public virtual LotesInsumos Lote { get; set; } = null!;

    public virtual Plantios Plantio { get; set; } = null!;
}
