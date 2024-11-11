using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plantech.DTOs;

namespace Plantech.ViewModels
{
    public class PlantioColheitaViewModel
{
    public IEnumerable<PlantioDTO> Plantios { get; set; }
    public IEnumerable<ColheitaDTO> Colheitas { get; set; }
}

}