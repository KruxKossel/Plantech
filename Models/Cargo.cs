using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class Cargo
{
    public int Id { get; set; }

    public string Funcao { get; set; } = null!;

    public string? Descricao { get; set; }

    public virtual ICollection<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();
}
