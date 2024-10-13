using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Plantech.ViewModels
{
    public class HortalicaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        public string? Descricao { get; set; }

        public string? Observacoes { get; set; }

        public string? CaminhoImagem { get; set; }

        [Display(Name = "Imagem")]
        public IFormFile? ImagemArquivo { get; set; }
    }
}
