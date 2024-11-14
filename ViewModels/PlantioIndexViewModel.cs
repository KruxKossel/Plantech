using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plantech.DTOs;

namespace Plantech.ViewModels
{
    public class PlantioIndexViewModel
    {
        public int Id { get; set; }
        public DateOnly? DataPlantio { get; set; }

        
        public int HortalicaId { get; set; }

        
        public int FuncionarioId { get; set; }

        public int? Quantidade { get; set; }

        
        public string Status { get; set; } = null!;

        
        public List<InsumosPlantioDTO> InsumosPlantios { get; set; } = new List<InsumosPlantioDTO>();

        public FuncionarioDTO Funcionario { get; set; }

       
         public HortalicaDTO Hortalica { get; set; }


        // guarda lotes insumo que serão listados
        public List<LotesInsumoDTO> LotesInsumos { get; set; } = new List<LotesInsumoDTO>();
    }
}