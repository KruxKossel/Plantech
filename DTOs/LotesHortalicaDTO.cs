using System;
using System.Collections.Generic;

namespace Plantech.DTOs;

public partial class LotesHortalicaDTO
{
    public int Id { get; set; }

    public int HortalicaId { get; set; }

    public int? Quantidade { get; set; }

    public double? PrecoVenda { get; set; }

    public DateOnly? DataEntrada { get; set; }

    public DateOnly? DataValidade { get; set; }

    public string Status { get; set; } = null!;

    public string Nome { get; set; } = null!;

    // public virtual ICollection<Colheitas> Colheitas { get; set; } = new List<Colheitas>();

    public virtual HortalicaDTO Hortalica { get; set; } = null!;

    // public virtual ICollection<HortalicasVendas> HortalicasVenda { get; set; } = new List<HortalicasVendas>();
}
