using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class Colheitas
{
    public int Id { get; set; }

    public int? Quantidade { get; set; }

    public DateOnly? DataColheita { get; set; }

    public int FuncionarioId { get; set; }

    public int? LoteHortalicaId { get; set; }

    public int? LoteInsumoId { get; set; }

    public int PlantioId { get; set; }

    public virtual ICollection<CulturasPerdidas> CulturasPerdidas { get; set; } = new List<CulturasPerdidas>();

    public virtual Funcionarios Funcionario { get; set; } = null!;

    public virtual LotesHortalicas? LoteHortalica { get; set; }

    public virtual LotesInsumos? LoteInsumo { get; set; }

    public virtual Plantios Plantio { get; set; } = null!;
}
