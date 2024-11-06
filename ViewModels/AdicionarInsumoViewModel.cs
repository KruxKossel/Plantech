using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plantech.ViewModels
{
    public class AdicionarInsumoViewModel
    {
        public int OrdemCompraId { get; set; }
        public List<InsumoViewModel> InsumosDisponiveis { get; set; } = new();

        public List<InsumoDadosViewModel> DadosInsumos { get; set; } = new();
    }

    public class InsumoDadosViewModel
    {
        public int InsumoId { get; set; }
        public int QtdInsumos { get; set; }
        public double PrecoUnitario { get; set; }
        public bool Selecionado { get; set; }
    }
}
