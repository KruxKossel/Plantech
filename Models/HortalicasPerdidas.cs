using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class HortalicasPerdidas
{
    public int CulturaPerdidaId { get; set; }

    public int HortalicaId { get; set; }

    public int? Quantidade { get; set; }

    public virtual CulturasPerdidas CulturaPerdida { get; set; } = null!;

    public virtual Hortalicas Hortalica { get; set; } = null!;
}
