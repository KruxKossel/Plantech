using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class Plantio
{
    public int Id { get; set; }

    public DateOnly? DataPlantio { get; set; }

    public int HortalicaId { get; set; }

    public int FuncionarioId { get; set; }

    public int? Quantidade { get; set; }

    public virtual ICollection<Colheita> Colheita { get; set; } = new List<Colheita>();

    public virtual Funcionario Funcionario { get; set; } = null!;

    public virtual Hortalica Hortalica { get; set; } = null!;

    public virtual ICollection<InsumosPlantio> InsumosPlantios { get; set; } = new List<InsumosPlantio>();
}
