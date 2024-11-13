using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plantech.DTOs
{
    public class HortalicasPerdidasDTO
    {
        public int CulturaPerdidaId { get; set; } 
        public int HortalicaId { get; set; } 
        public int? Quantidade { get; set; }

        public CulturasPerdidasDTO CulturaPerdida { get; set; } = null!;

        public HortalicaDTO Hortalica { get; set; } = null!;
    }
}