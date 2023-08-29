using webapi.Filmes.Properties.Domains;

namespace webapi.Filmes.Properties.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório GeneroRepository
    /// Definir os métodos que serão implementados pelo GeneroRepository
    /// </summary>
    public interface IGeneroRepository
    {
        //tipoRetorno NomeMetodo(tipoParâmetro nomeParâmetro) 

        /// <summary>
        /// Cadastrar um gênero
        /// </summary>
        /// <param name="novoGenero"> Objeto que será cadastrado</param>
        void Cadastrar(GeneroDomain novoGenero);

        /// <summary>
        /// Lista todos os generos cadastrados
        /// </summary>
        /// <returns>Lista todos como objetos</returns>
        List<GeneroDomain> ListarTodos();

        /// <summary>
        /// Atualiza o objeto existente passando seu id pelo corpo da requisição
        /// </summary>
        /// <param name="genero">Objeto atualizado (novas informações)</param>
        void AtualizarIdCorpo(GeneroDomain genero);

        /// <summary>
        /// Atualiza o objeto existente passando seu id pela url
        /// </summary>
        /// <param name="id">Id do objeto que sera atualizado</param>
        /// <param name="genero">Objeto atualizado (novas informações)</param>
        void AtualizarIdUrl(int id,GeneroDomain genero);

        /// <summary>
        /// Deletar um objeto
        /// </summary>
        /// <param name="id">Id do objeto que será deletado</param>
        void Deletar(int id);

        /// <summary>
        /// Busca um objeto pelo id
        /// </summary>
        /// <param name="id"> Id do objeto que sera buscado</param>
        /// <returns>Objeto que será encontrado</returns>
        GeneroDomain BuscarPorId(int id);
    }
}
