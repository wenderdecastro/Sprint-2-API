using System.ComponentModel.DataAnnotations;

namespace webapi.Filmes.Properties.Domains
{
    /// <summary>
    /// Classe que representa a entidade (Tabela) Genero
    /// </summary>
    
    public class GeneroDomain
    {
        [Required]
        public int IdGenero { get; set; }

        [Required(ErrorMessage = "O nome do genero é obrigatório!")]
        public string? Nome { get; set; }
    }
}
