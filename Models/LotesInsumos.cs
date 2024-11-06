using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class LotesInsumos
{
    public int Id { get; set; }

    public int InsumoId { get; set; }

    public int? Quantidade { get; set; }

    public double? PrecoUnitario { get; set; }

    public string? DataEntrada { get; set; }

    public string? DataValidade { get; set; }

    public string Status { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public virtual ICollection<Colheitas> Colheitas { get; set; } = new List<Colheitas>();

    public virtual Insumos Insumo { get; set; } = null!;

    public virtual ICollection<InsumosCompras> InsumosCompras { get; set; } = new List<InsumosCompras>();

    public virtual ICollection<InsumosPlantios> InsumosPlantios { get; set; } = new List<InsumosPlantios>();
}
