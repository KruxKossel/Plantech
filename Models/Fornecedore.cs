﻿using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class Fornecedore
{
    public int Id { get; set; }

    public string Cnpj { get; set; } = null!;

    public string RazaoSocial { get; set; } = null!;

    public string? Cidade { get; set; }

    public string? Endereco { get; set; }

    public string? Email { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Insumo> Insumos { get; set; } = new List<Insumo>();

    public virtual ICollection<OrdensCompra> OrdensCompras { get; set; } = new List<OrdensCompra>();
}
