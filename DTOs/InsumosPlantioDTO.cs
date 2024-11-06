using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plantech.DTOs
{
    public class InsumosPlantioDTO
    {
        public int PlantioId { get; set; }

        public int LoteId { get; set; }

        public int? Quantidade { get; set; }
    }
}