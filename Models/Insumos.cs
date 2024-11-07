using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class Insumos
{
    public int Id { get; set; }

    public int? FornecedorId { get; set; }

    public string Nome { get; set; } = null!;

    public string? Descricao { get; set; }

    public string? Observacoes { get; set; }

    public string? Categoria { get; set; }

    public string Tipo { get; set; } = null!;

    public string? CaminhoImagem { get; set; }

    public string Status { get; set; } = "ativo";

    public virtual Fornecedores? Fornecedor { get; set; }

    public virtual ICollection<InsumosCompras> InsumosCompras { get; set; } = new List<InsumosCompras>();

    public virtual ICollection<LotesInsumos> LotesInsumos { get; set; } = new List<LotesInsumos>();
}
