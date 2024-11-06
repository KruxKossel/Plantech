using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plantech.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        public string NomeUsuario { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Senha { get; set; } = null!;

        public string Salt { get; set; } = null!;

        public string Status { get; set; } = null!;
        public List<FuncionarioDTO> Funcionarios { get; internal set; }

    }
}