﻿using System;
using System.Collections.Generic;
using Plantech.Models;
namespace Plantech.DTOs;

public partial class InsumoDTO
{
    public int Id { get; set; }

    public int? FornecedorId { get; set; }

    public string Nome { get; set; } = null!;

    public string? Descricao { get; set; }

    public string? Observacoes { get; set; }

    public string? Categoria { get; set; }

    public string Tipo { get; set; } = null!;
    
    public string Status { get; set; } = "ativo";

    public string? CaminhoImagem { get; set; }

    public FornecedoreDTO? Fornecedor { get; set; }
    // public double PrecoUnitario { get; set; } 

    // public int Quantidade { get; set; } 

}
