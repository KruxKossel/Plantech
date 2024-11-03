using Plantech.Models;

namespace Plantech.DTOs;


public partial class OrdensCompraDTO
{
   
    public int Id { get; set; }
    public double? Total { get; set; }
    public string Status { get; set; } = null!;
    public int FuncionarioId { get; set; }
    public int FornecedorId { get; set; }
    public DateOnly? DataCompra { get; set; }
    public virtual FornecedoreDTO Fornecedor { get; set; } = null!;
    public virtual FuncionarioDTO Funcionario { get; set; } = null!;
     public virtual ICollection<InsumosCompra> InsumosCompras { get; set; } = new List<InsumosCompra>();
}
