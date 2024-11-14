using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plantech.DTOs;

namespace Plantech.ViewModels
{
    public class CulturaPerdidaViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public ColheitaDTO Colheita { get; set; } = null!;
    public ICollection<HortalicasPerdidasDTO> HortalicasPerdidas { get; set; } = new List<HortalicasPerdidasDTO>();
}

public class HortalicaPerdidaViewModel
{
    public string NomeHortalica { get; set; } = null!; // Nova propriedade
    public int? Quantidade { get; set; }
}

}