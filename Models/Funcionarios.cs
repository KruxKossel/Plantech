using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class Funcionarios
{
    public int Id { get; set; }

    public string Cpf { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public int UsuarioId { get; set; }

    public int CargoId { get; set; }

    public string Status { get; set; } = null!;

    public virtual Cargos Cargo { get; set; } = null!;

    public virtual ICollection<Colheitas> Colheitas { get; set; } = new List<Colheitas>();

    public virtual ICollection<OrdensCompras> OrdensCompras { get; set; } = new List<OrdensCompras>();

    public virtual ICollection<Plantios> Plantios { get; set; } = new List<Plantios>();

    public virtual Usuarios Usuario { get; set; } = null!;

    public virtual ICollection<Vendas> Vendas { get; set; } = new List<Vendas>();
}
