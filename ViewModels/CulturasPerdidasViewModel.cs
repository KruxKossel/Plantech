using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plantech.DTOs;

namespace Plantech.ViewModels
{
    public class CulturasPerdidasViewModel
    {
        public virtual List<HortalicasPerdidasDTO> HortalicasPerdidas { get; set; } = new List<HortalicasPerdidasDTO>();

         public virtual CulturasPerdidasDTO CulturaPerdida { get; set; } = null!;


    }
}