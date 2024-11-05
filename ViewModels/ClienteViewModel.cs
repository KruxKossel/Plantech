using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plantech.ViewModels
{
    public partial class ClienteViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "CNPJ é obrigatório")]
        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; } = null!;

        [Required(ErrorMessage = "Razão Social é obrigatória")]
        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; } = null!;

        [Display(Name = "Endereço")]
        public string? Endereco { get; set; }

        [Display(Name = "Telefone")]
        public string? Telefone { get; set; }

        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Status é obrigatório")]
        [Display(Name = "Status")]
        public string Status { get; set; } = null!;
    }
}
