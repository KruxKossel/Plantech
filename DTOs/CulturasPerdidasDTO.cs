using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plantech.DTOs
{
    public class CulturasPerdidasDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public int ColheitaId { get; set; }

        public virtual ColheitaDTO Colheita { get; set; } = null!;

        public virtual ICollection<HortalicasPerdidasDTO> HortalicasPerdidas { get; set; } = new List<HortalicasPerdidasDTO>();
    }
}