using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Plantech.ViewModels;

public partial class LotesInsumoViewModel
{
    public int Id { get; set; }

    public int InsumoId { get; set; }

    public int? Quantidade { get; set; }

    public double? PrecoUnitario { get; set; }

    public DateOnly? DataEntrada { get; set; }

    public DateOnly? DataValidade { get; set; }
    // [HiddenInput(DisplayValue = false)]
    public string Status { get; set; }
    public string Nome { get; set; } = null!;

}
