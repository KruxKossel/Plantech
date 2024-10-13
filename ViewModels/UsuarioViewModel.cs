using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Plantech.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }

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
        public string Status { get; set; }
    }
}