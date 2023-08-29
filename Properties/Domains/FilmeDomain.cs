using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Filmes.Properties.Domains
{
    public class FilmeDomain
    {
        [Required]
        public int IdFilme { get; set; }

        [Required]
        public int IdGenero { get; set; }

        [Required(ErrorMessage = "O nome do filme é obrigatório!")]
        public string Titulo { get; set; }

        public GeneroDomain Genero { get; set; }


    }
}
