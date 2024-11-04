using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Plantech.ViewModels;

public class AdicionarInsumoViewModel
{
    public int OrdemCompraId { get; set; }  // ID da Ordem de Compra

    // Lista dos insumos disponíveis para seleção
    public IEnumerable<InsumoViewModel> InsumosDisponiveis { get; set; }

    // IDs dos insumos selecionados pelo usuário
    public int[] SelectedInsumos { get; set; }

    // Dados adicionais para cada insumo selecionado (quantidade e preço unitário)
    public Dictionary<int, InsumoDetalhes> Dados { get; set; }

    // Propriedades do insumo selecionado (para vinculação do formulário)
    public InsumoViewModel InsumoSelecionado { get; set; }
}

// Classe auxiliar para organizar os detalhes dos insumos selecionados
public class InsumoDetalhes
{
    public int QtdInsumos { get; set; }
    public double PrecoUnitario { get; set; }
}
