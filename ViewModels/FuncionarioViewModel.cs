using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Plantech.ViewModels
{
    public class FuncionarioViewModel
    {
        public string Id { get; set;}



        // Usuario
        [Required]
        [StringLength(50, ErrorMessage = "O nome de usuário não pode exceder 50 caracteres.")]
        public string NomeUsuario { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required]
        public string Status { get; set; } = null!;

        // Funcionario
        public string Cpf { get; set; } = null!;

        public string Nome { get; set; } = null!;

    }
}