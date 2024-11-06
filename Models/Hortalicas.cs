using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class Hortalicas
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string? Descricao { get; set; }

    public string? Observacoes { get; set; }

    public string? CaminhoImagem { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<HortalicasPerdidas> HortalicasPerdidas { get; set; } = new List<HortalicasPerdidas>();

    public virtual ICollection<LotesHortalicas> LotesHortalicas { get; set; } = new List<LotesHortalicas>();

    public virtual ICollection<Plantios> Plantios { get; set; } = new List<Plantios>();
}
