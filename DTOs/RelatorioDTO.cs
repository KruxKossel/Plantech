using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plantech.Models;

namespace Plantech.DTOs
{
    public class RelatorioDTO
    {
        public virtual IEnumerable<OrdensCompraDTO> Compras {get;set;} = new List<OrdensCompraDTO>();
        public virtual IEnumerable<VendaDTO> Venda {get;set;} = new List<VendaDTO>();
        public virtual IEnumerable<PlantioDTO> Plantio {get;set;} = new List<PlantioDTO>();
        public virtual IEnumerable<ColheitaDTO> Colheita {get;set;} = new List<ColheitaDTO>();
        
    }
}