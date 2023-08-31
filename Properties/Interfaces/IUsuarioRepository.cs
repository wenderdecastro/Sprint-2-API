using webapi.Filmes.Properties.Domains;

namespace webapi.Filmes.Properties.Interfaces
{
    public interface IUsuarioRepository
    {

        /// <summary>
        /// Método de autenticação de usuário
        /// </summary>
        /// <param name="email"> email do usuario </param>
        /// <param name="password"> senha do usuario </param>
        /// <returns>retorna um objeto do tipo usuario</returns>s
        UsuarioDomain Login(string email, string password);
    }
}
