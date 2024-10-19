using System;
using System.Collections.Generic;
using Plantech.Models;

namespace Plantech.DTOs;

public partial class HortalicaDTO
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string? Descricao { get; set; }

    public string? Observacoes { get; set; }

    public string? CaminhoImagem { get; set; }
}
