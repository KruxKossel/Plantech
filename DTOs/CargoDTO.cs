using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plantech.DTOs
{
    public class CargoDTO
    {
        public int Id { get; set; }

        public string Funcao { get; set; } = null!;

        public string? Descricao { get; set; }

        public virtual ICollection<FuncionarioDTO> Funcionarios { get; set; } = new List<FuncionarioDTO>();

        
    }
}