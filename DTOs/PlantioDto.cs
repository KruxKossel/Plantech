using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plantech.DTOs
{
    public class PlantioDTO
    {
        public int Id { get; set; }
        public DateOnly? DataPlantio { get; set; }
        public int HortalicaId { get; set; }
        public int FuncionarioId { get; set; }
        public int? Quantidade { get; set; }

        public string Status { get; set; } = null!;

        public List<InsumosPlantioDTO> InsumosPlantios { get; set; } = new List<InsumosPlantioDTO>();

        public HortalicaDTO Hortalica { get; set; } = null!;


    }
}