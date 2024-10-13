using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class Hortalica
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string? Descricao { get; set; }

    public string? Observacoes { get; set; }

    public string? CaminhoImagem { get; set; }

    public virtual ICollection<HortalicasPerdida> HortalicasPerdida { get; set; } = new List<HortalicasPerdida>();

    public virtual ICollection<LotesHortalica> LotesHortalicas { get; set; } = new List<LotesHortalica>();

    public virtual ICollection<Plantio> Plantios { get; set; } = new List<Plantio>();
}
