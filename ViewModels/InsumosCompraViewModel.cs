﻿using System;
using System.Collections.Generic;
using Plantech.DTOs;

namespace Plantech.ViewModels;

public partial class InsumosCompraViewModel
{
    public int OrdemCompraId { get; set; }

    public int InsumoId { get; set; }

    public int? LoteId { get; set; }

    public int? Quantidade { get; set; }

    public double? PrecoUnitario { get; set; }

    public DateOnly? DataChegada { get; set; }

     public virtual ICollection<InsumosCompraDTO> InsumosCompras { get; set; } = new List<InsumosCompraDTO>();

    // public virtual Insumo Insumo { get; set; } = null!;

    // public virtual LotesInsumo Lote { get; set; } = null!;

    // public virtual OrdensCompra OrdemCompra { get; set; } = null!;
}
