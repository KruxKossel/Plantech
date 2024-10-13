using System;
using System.Collections.Generic;

namespace Plantech.Models;

public partial class Insumo
{
    public int Id { get; set; }

    public int? FornecedorId { get; set; }

    public string Nome { get; set; } = null!;

    public string? Descricao { get; set; }

    public string? Observacoes { get; set; }

    public string? Categoria { get; set; }

    public string Tipo { get; set; } = null!;

    public string? CaminhoImagem { get; set; }

    public virtual Fornecedore? Fornecedor { get; set; }

    public virtual ICollection<InsumosCompra> InsumosCompras { get; set; } = new List<InsumosCompra>();

    public virtual ICollection<LotesInsumo> LotesInsumos { get; set; } = new List<LotesInsumo>();
}
