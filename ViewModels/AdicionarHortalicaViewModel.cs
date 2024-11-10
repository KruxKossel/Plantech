using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Plantech.Models;

namespace Plantech.ViewModels
{
    public class AdicionarHortalicaViewModel
    {
        public List<LotesHortalicaViewModel> LotesDisponiveis { get; set; } = new();
        public List<HortalicasVendaViewModel> LotesSelecionados { get; set; } = new();
        
    }

}
