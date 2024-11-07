using System.ComponentModel;
using Plantech.DTOs;

namespace Plantech.ViewModels;

public partial class VendaViewModel

{
    public int Id { get; set; }
    [DisplayName("Data da venda")]
    public DateOnly? Data { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    [DisplayName("Total da Venda")]
    public double? TotalVendas { get; set; }

    public int? QuantidadeProdutos { get; set; }

    public string Status { get; set; } = "pago";

    public int? ClienteId { get; set; }

    public int? FuncionarioId { get; set; }

    public virtual ClienteDTO? Cliente { get; set; } = null!;

    // public int? CnpjCliente {get;set;}

    public virtual FuncionarioDTO? Funcionario { get; set; } = null!;
    
    // public string NomeFuncionario {get;set;} = null!;

    // public virtual ICollection<HortalicasVendas> HortalicasVendas { get; set; } = new List<HortalicasVendas>();
}
