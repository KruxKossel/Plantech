using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class Permisso
{
    public int Id { get; set; }

    public int CargoId { get; set; }

    public int TelaId { get; set; }

    public int? CanCreate { get; set; }

    public int? CanRead { get; set; }

    public int? CanUpdate { get; set; }

    public int? CanDelete { get; set; }

    public virtual Cargo Cargo { get; set; } = null!;

    public virtual Tela Tela { get; set; } = null!;
}
