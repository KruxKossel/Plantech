using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string NomeUsuario { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ICollection<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();
}
