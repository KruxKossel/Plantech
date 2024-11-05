using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plantech.DTOs
{
    public class LotesHortalicaDTO
    {
    public int Id { get; set; }

    public int HortalicaId { get; set; }

    public int? Quantidade { get; set; }

    public double? PrecoVenda { get; set; }

    public DateOnly? DataEntrada { get; set; }

    public DateOnly? DataValidade { get; set; }

    public string Status { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public List<ColheitaDTO> Colheita { get; set; } = new List<ColheitaDTO>();

    public HortalicaDTO Hortalica { get; set; } = null!;

    }
}