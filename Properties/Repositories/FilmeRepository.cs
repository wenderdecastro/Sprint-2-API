using System.Data.SqlClient;
using webapi.Filmes.Properties.Domains;
using webapi.Filmes.Properties.Interfaces;

namespace webapi.Filmes.Properties.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {

        private string StringConexao = "Data Source=NOTE10-S15; Initial Catalog=Filmes; User ID =sa; Pwd =Senai@134";

        public void AtualizarIdCorpo(FilmeDomain filme)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //string de insert em TSQL que irá inserir os dados na tabela
                string queryInsert = $"UPDATE Filme SET Titulo = @titulo, IdGenero = @idgenero WHERE IdFilme = @idfilme";

                //abre conexão com o banco de dados
                con.Open();

                //instancia um objeto para utilizar dos metodos que permitem acesso ao sql
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {

                    cmd.Parameters.AddWithValue("@idfilme", filme.IdFilme);
                    cmd.Parameters.AddWithValue("@idgenero", filme.IdGenero);
                    cmd.Parameters.AddWithValue("@titulo", filme.Titulo);

                    //comando executara a query insert
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarIdUrl(int id, FilmeDomain filme)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //abre conexão com o banco de dados
                con.Open();

                //string de Busca em TSQL que irá inserir os dados na tabela
                string queryById = $"UPDATE Filme SET Titulo = @titulo, IdGenero = @idgenero WHERE IdFilme = @idfilme";

                //instancia um objeto para utilizar dos metodos que permitem acesso ao sql
                using (SqlCommand cmd = new SqlCommand(queryById, con))
                {
                    cmd.Parameters.AddWithValue("@idfilme", id);
                    cmd.Parameters.AddWithValue("@idgenero", filme.IdGenero);
                    cmd.Parameters.AddWithValue("@titulo", filme.Titulo);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public FilmeDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // declara a instrução a ser utilizada no sql
                string querySelectById = "SELECT Filme.IdFilme, Filme.IdGenero, Filme.Titulo, Genero.Nome, Genero.IdGenero FROM Filme LEFT JOIN Genero ON Filme.IdGenero = Genero.IdGenero WHERE IdFilme = 1";

                // Abre a conexão com o banco de dados
                con.Open();

                SqlDataReader rdr;

                // Declara o sqlcommand que fará uma consulta utilizando o querySelectById e a conexão desejada
                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        FilmeDomain filmeBuscado = new FilmeDomain()
                        {
                            IdFilme = Convert.ToInt32(rdr[0]),

                            IdGenero = Convert.ToInt32(rdr[1]),

                            Titulo = rdr["Titulo"].ToString(),

                            Genero = new GeneroDomain()
                            {
                                IdGenero = Convert.ToInt32(rdr[4]),

                                Nome = rdr["Nome"].ToString()
                            }

                        };

                        return filmeBuscado;

                    }
                    return null;
                }

            }

        }

        public void Cadastrar(FilmeDomain novoFilme)
        {

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();

                string queryInsert = "INSERT INTO Filme(IdGenero, Titulo) VALUES(@IdGenero, @Titulo)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@Titulo", novoFilme.Titulo);
                    cmd.Parameters.AddWithValue("@IdGenero", novoFilme.IdGenero);

                    //comando executara a query insert
                    cmd.ExecuteNonQuery();

                }

            }

        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //string de insert em TSQL que irá inserir os dados na tabela
                string queryDelete = $"DELETE FROM Filme WHERE IdFilme = @idfilme";

                //abre conexão com o banco de dados
                con.Open();

                //instancia um objeto para utilizar dos metodos que permitem acesso ao sql
                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {

                    cmd.Parameters.AddWithValue("@idfilme", id);

                    //comando executara a query insert
                    cmd.ExecuteNonQuery();

                }

            }

        }

        public List<FilmeDomain> ListarTodos()
        {
            List<FilmeDomain> ListaFilmes = new List<FilmeDomain>();

            //Declara a sqlconnection utlizando a string conexao como parametro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // declara a instrução a ser utilizada no sql
                string querySelectAll = "SELECT Filme.IdFilme, Filme.IdGenero, Filme.Titulo, Genero.Nome, Genero.IdGenero FROM Filme LEFT JOIN Genero ON Filme.IdGenero = Genero.IdGenero";

                // Abre a conexão com o banco de dados
                con.Open();

                SqlDataReader rdr;

                // Declara o sqlcommand que fará uma consulta utilizando o queryselectall e a conexão desejada
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        FilmeDomain filme = new FilmeDomain()
                        {
                            IdFilme = Convert.ToInt32(rdr[0]),

                            IdGenero = Convert.ToInt32(rdr[1]),

                            Titulo = rdr["Titulo"].ToString(),

                            Genero = new GeneroDomain()
                            {
                                IdGenero = Convert.ToInt32(rdr[4]),

                                Nome = rdr["Nome"].ToString()
                            }

                        };

                        ListaFilmes.Add(filme);

                    }

                }

            }

            return ListaFilmes;

        }
    }
}
