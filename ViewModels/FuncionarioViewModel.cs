using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Plantech.DTOs;

namespace Plantech.ViewModels
{
    public class FuncionarioViewModel
    {
        public int Id { get; set; }

        // Usuario

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

        public  UsuarioDTO Usuario { get; set; }



        // Funcionario
        [Display(Name = "CPF")]
        public string Cpf { get; set; } = null!;

        [Display(Name = "Nome")]
        public string Nome { get; set; } = null!;

         

        // Cargo

         public CargoDTO Cargo { get; set; }

        // [Display(Name = "Função")]

        // public string Funcao {get;set;}


        // [Display(Name = "Descrição")]
        // public string Descricao { get; set; }


    }
}
