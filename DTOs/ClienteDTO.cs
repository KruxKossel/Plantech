using System;
using System.Collections.Generic;
using Plantech.Models;

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
}
