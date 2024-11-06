using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class Plantios
{
    public int Id { get; set; }

    public string? DataPlantio { get; set; }

    public int HortalicaId { get; set; }

    public int FuncionarioId { get; set; }

    public int? Quantidade { get; set; }

    public string? Status { get; set; }

    public virtual Colheitas? Colheitas { get; set; }

    public virtual Funcionarios Funcionario { get; set; } = null!;

    public virtual Hortalicas Hortalica { get; set; } = null!;

    public virtual ICollection<InsumosPlantios> InsumosPlantios { get; set; } = new List<InsumosPlantios>();
}
