using System;
using System.Collections.Generic;

namespace Plantech.ViewModels;

public partial class LotesInsumoViewModel
{
    public int Id { get; set; }

    public int InsumoId { get; set; }

    public int? Quantidade { get; set; }

    public double? PrecoUnitario { get; set; }

    public DateOnly? DataEntrada { get; set; }

    public DateOnly? DataValidade { get; set; }

    public string Status { get; set; } = null!;

    public string Nome { get; set; } = null!;

}
