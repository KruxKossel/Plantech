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

    // public virtual ICollection<Colheitas> Colheitas { get; set; } = new List<Colheitas>();

    public virtual ICollection<OrdensCompraDTO> OrdensCompras { get; set; } = new List<OrdensCompraDTO>();

    // public virtual ICollection<Plantios> Plantios { get; set; } = new List<Plantios>();

    // public virtual Usuarios Usuario { get; set; } = null!;

    // public virtual ICollection<Vendas> Venda { get; set; } = new List<Vendas>();
    }
}