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

        [Required]
        [StringLength(50, ErrorMessage = "O nome de usuário não pode exceder 50 caracteres.")]
        [Display(Name = "Nome de Usuário")]
        public string NomeUsuario { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; } = null!;



        // Funcionario
        [Display(Name = "CPF")]
        public string Cpf { get; set; } = null!;

        [Display(Name = "Nome")]
        public string Nome { get; set; } = null!;

        // apêndice

        [Required]
        [Display(Name = "ID do Cargo")]
        public int CargoId { get; set; } 
    }
}