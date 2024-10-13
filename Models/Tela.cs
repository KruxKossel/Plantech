using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class Tela
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Permisso> Permissos { get; set; } = new List<Permisso>();
}
