using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class Funcionario
{
    public int Id { get; set; }

    public string Cpf { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public int UsuarioId { get; set; }

    public int CargoId { get; set; }

    public string Status { get; set; } = null!;

    public virtual Cargo Cargo { get; set; } = null!;

    public virtual ICollection<Colheita> Colheita { get; set; } = new List<Colheita>();

    public virtual ICollection<OrdensCompra> OrdensCompras { get; set; } = new List<OrdensCompra>();

    public virtual ICollection<Plantio> Plantios { get; set; } = new List<Plantio>();

    public virtual Usuario Usuario { get; set; } = null!;

    public virtual ICollection<Venda> Venda { get; set; } = new List<Venda>();
}
