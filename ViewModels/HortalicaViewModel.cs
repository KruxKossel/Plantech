using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Plantech.ViewModels
{
    public class HortalicaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = null!;

        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        [Display(Name = "Observações")]
        public string? Observacoes { get; set; }

        [Display(Name = "Caminho da Imagem")]
        public string? CaminhoImagem { get; set; }

        [Display(Name = "Imagem")]
        public IFormFile? ImagemArquivo { get; set; }
    }
}
