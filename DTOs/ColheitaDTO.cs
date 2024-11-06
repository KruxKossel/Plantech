using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plantech.DTOs
{
    public class ColheitaDTO
    {
    public int Id { get; set; }

    public int? Quantidade { get; set; }

    public DateOnly? DataColheita { get; set; }

    public int FuncionarioId { get; set; }

    public int? LoteHortalicaId { get; set; }

    public int? LoteInsumoId { get; set; }

    public int PlantioId { get; set; }

    public FuncionarioDTO Funcionario { get; set; } = null!;

    public LotesHortalicaDTO? LoteHortalica { get; set; }

    public LotesInsumoDTO? LoteInsumo { get; set; }

    public PlantioDTO Plantio { get; set; } = null!;
    }
}