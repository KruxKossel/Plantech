using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class Colheita
{
    public int Id { get; set; }

    public int? Quantidade { get; set; }

    public DateOnly? DataColheita { get; set; }

    public int FuncionarioId { get; set; }

    public int? LoteHortalicaId { get; set; }

    public int? LoteInsumoId { get; set; }

    public int PlantioId { get; set; }

    public virtual ICollection<CulturasPerdida> CulturasPerdida { get; set; } = new List<CulturasPerdida>();

    public virtual Funcionario Funcionario { get; set; } = null!;

    public virtual LotesHortalica? LoteHortalica { get; set; }

    public virtual LotesInsumo? LoteInsumo { get; set; }

    public virtual Plantio Plantio { get; set; } = null!;
}
