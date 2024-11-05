﻿using System;
using System.Collections.Generic;
using Plantech.DTOs;

namespace Plantech.ViewModels;

public partial class LotesHortalicaViewModel
{
    public int Id { get; set; }

    public int HortalicaId { get; set; }

    public int? Quantidade { get; set; }

    public double? PrecoVenda { get; set; }

    public DateOnly? DataEntrada { get; set; }

    public DateOnly? DataValidade { get; set; }

    public string Status { get; set; } = null!;

    public string Nome { get; set; } = null!;

    // public virtual ICollection<Colheita> Colheita { get; set; } = new List<Colheita>();

     public virtual HortalicaDTO Hortalica { get; set; } = null!;

    // public virtual ICollection<HortalicasVenda> HortalicasVenda { get; set; } = new List<HortalicasVenda>();
}
