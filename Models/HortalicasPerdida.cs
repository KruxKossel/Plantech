using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class HortalicasPerdida
{
    public int CulturaPerdidaId { get; set; }

    public int HortalicaId { get; set; }

    public int? Quantidade { get; set; }

    public virtual CulturasPerdida CulturaPerdida { get; set; } = null!;

    public virtual Hortalica Hortalica { get; set; } = null!;
}
