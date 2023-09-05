using System.ComponentModel.DataAnnotations;

namespace webapi.Filmes.Properties.Domains
{
    public class UsuarioDomain
    {
        [Required]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "O campo de Email é obrigatório!")]
        public string Email { get; set; }


        [StringLength(20, MinimumLength = 3, ErrorMessage = "A senha deve ter de 3, a 20 caracteres")]
        [Required(ErrorMessage = "O campo de senha é obrigatório!")]
        public string Senha { get; set; }

        public bool Permissao { get; set; }






    }
}
