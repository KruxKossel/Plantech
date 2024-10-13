using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class CulturasPerdida
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public int ColheitaId { get; set; }

    public virtual Colheita Colheita { get; set; } = null!;

    public virtual ICollection<HortalicasPerdida> HortalicasPerdida { get; set; } = new List<HortalicasPerdida>();
}
