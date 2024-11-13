using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plantech.DTOs;

namespace Plantech.ViewModels
{
    public class CulturasPerdidasViewModel
    {
        public int? Quantidade { get; set; }

        public virtual CulturasPerdidasDTO CulturaPerdida { get; set; } = null!;

        public virtual HortalicaDTO Hortalica { get; set; } = null!;


    }
}