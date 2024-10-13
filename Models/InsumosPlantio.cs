using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class InsumosPlantio
{
    public int PlantioId { get; set; }

    public int LoteId { get; set; }

    public int? Quantidade { get; set; }

    public virtual LotesInsumo Lote { get; set; } = null!;

    public virtual Plantio Plantio { get; set; } = null!;
}
