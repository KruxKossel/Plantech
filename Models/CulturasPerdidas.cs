using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class CulturasPerdidas
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public int ColheitaId { get; set; }

    public virtual Colheitas Colheita { get; set; } = null!;

    public virtual ICollection<HortalicasPerdidas> HortalicasPerdidas { get; set; } = new List<HortalicasPerdidas>();
}
