using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class LotesInsumo
{
    public int Id { get; set; }

    public int InsumoId { get; set; }

    public int? Quantidade { get; set; }

    public double? PrecoUnitario { get; set; }

    public DateOnly? DataEntrada { get; set; }

    public DateOnly? DataValidade { get; set; }

    public string Status { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public virtual ICollection<Colheita> Colheita { get; set; } = new List<Colheita>();

    public virtual Insumo Insumo { get; set; } = null!;

    public virtual ICollection<InsumosCompra> InsumosCompras { get; set; } = new List<InsumosCompra>();

    public virtual ICollection<InsumosPlantio> InsumosPlantios { get; set; } = new List<InsumosPlantio>();
}
