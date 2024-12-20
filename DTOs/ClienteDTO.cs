﻿using System;
using System.Collections.Generic;

namespace Plantech.DTOs;

public partial class ClienteDTO
{
    public int Id { get; set; }

    public string Cnpj { get; set; } = null!;

    public string RazaoSocial { get; set; } = null!;

    public string? Endereco { get; set; }

    public string? Telefone { get; set; }

    public string? Email { get; set; }

    public string Status { get; set; } = null!;

    // public virtual ICollection<Vendas> Venda { get; set; } = new List<Vendas>();
}
