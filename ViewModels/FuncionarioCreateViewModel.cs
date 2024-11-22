using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Plantech.ViewModels
{
    public class FuncionarioCreateViewModel
    {
        public int Id { get; set; }

        // Usuario
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome de usuário não pode exceder 50 caracteres.")]
        [Display(Name = "Nome de Usuário")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O formato do email é inválido.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O status é obrigatório.")]
        [Display(Name = "Status")]
        public string Status { get; set; } = null!;

        // Funcionario
        [Display(Name = "CPF")]
        [RegularExpression(@"\d{3}\.\d{3}\.\d{3}-\d{2}", ErrorMessage = "O formato do CPF é inválido.")]
        public string Cpf { get; set; } = null!;

        [Display(Name = "Nome")]
        public string Nome { get; set; } = null!;

        // apêndice
        [Required(ErrorMessage = "O ID do cargo é obrigatório.")]
        [Display(Name = "ID do Cargo")]
        public int CargoId { get; set; }
    }
}
