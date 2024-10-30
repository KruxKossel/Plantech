using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plantech.DTOs
{
    public class LotesInsumoDTO
    {
        public int Id { get; set; }

        public int InsumoId { get; set; }

        public int? Quantidade { get; set; }

        public double? PrecoUnitario { get; set; }

        public DateOnly? DataEntrada { get; set; }

        public DateOnly? DataValidade { get; set; }

        public string Status { get; set; } = null!;

        public string Nome { get; set; } = null!;

        public InsumoDTO Insumo { get; set; } = null!;

    }
}