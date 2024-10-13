using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plantech.DTOs
{
    public class HortalicaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public string? Observacoes { get; set; }
        public string? CaminhoImagem { get; set; }
    }
}
