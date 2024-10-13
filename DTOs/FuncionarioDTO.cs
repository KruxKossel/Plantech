using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plantech.DTOs
{
    public class FuncionarioDTO
    {
        public int Id { get; set; }

        public string Cpf { get; set; } = null!;

        public string Nome { get; set; } = null!;

        public int UsuarioId { get; set; }

        public int CargoId { get; set; }

        public string Status { get; set; } = null!;
    }
}