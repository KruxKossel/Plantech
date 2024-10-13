using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class Alerta
{
    public int Id { get; set; }

    public int LoteId { get; set; }

    public string Tipo { get; set; } = null!;

    public string Mensagem { get; set; } = null!;

    public string DataCriacao { get; set; } = null!;
}
