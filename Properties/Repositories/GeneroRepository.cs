using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using webapi.Filmes.Properties.Domains;
using webapi.Filmes.Properties.Interfaces;

namespace webapi.Filmes.Properties.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {

        private string StringConexao = "Data Source=NOTE10-S15; Initial Catalog=Filmes; User ID =sa; Pwd =Senai@134";


        public void AtualizarIdCorpo(GeneroDomain genero)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //string de insert em TSQL que irá inserir os dados na tabela
                string queryInsert = $"UPDATE Genero SET Nome = @nome WHERE IdGenero = @idgenero";

                //abre conexão com o banco de dados
                con.Open();

                //instancia um objeto para utilizar dos metodos que permitem acesso ao sql
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {

                    cmd.Parameters.AddWithValue("@idgenero", genero.IdGenero);
                    cmd.Parameters.AddWithValue("@nome", genero.Nome);

                    //comando executara a query insert
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarIdUrl(int id, GeneroDomain genero)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //abre conexão com o banco de dados
                con.Open();

                //string de Busca em TSQL que irá inserir os dados na tabela
                string queryById = $"UPDATE Genero SET Nome = @nome WHERE idgenero = @idgenero";

                //instancia um objeto para utilizar dos metodos que permitem acesso ao sql
                using (SqlCommand cmd = new SqlCommand(queryById, con))
                {
                    cmd.Parameters.AddWithValue("@idgenero", id);
                    cmd.Parameters.AddWithValue("@nome", genero.Nome);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public GeneroDomain BuscarPorId(int id)
        {

            using (SqlConnection con = new SqlConnection(StringConexao))
            {

                //abre conexão com o banco de dados
                con.Open();

                //string de Busca em TSQL que irá inserir os dados na tabela
                string queryById = $"SELECT IdGenero, Nome FROM Genero WHERE genero.IdGenero = @idgenero";

                //instancia um objeto para utilizar dos metodos que permitem acesso ao sql
                using (SqlCommand cmd = new SqlCommand(queryById, con))
                {
                    cmd.Parameters.AddWithValue("@idgenero", id);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {

                        if (rdr.Read())
                        {
                            GeneroDomain generoBuscado = new GeneroDomain()
                            {
                                IdGenero = Convert.ToInt32(rdr[0]),

                                Nome = rdr["Nome"].ToString()

                            };
                            return generoBuscado;
                        }

                        return null;

                    }
                }
            }
        }

        /// <summary>
        /// Cadastrar um novo genero
        /// </summary>
        /// <param name="novoGenero">Objeto com as informações que serão cadastrados</param>
        public void Cadastrar(GeneroDomain novoGenero)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //string de insert em TSQL que irá inserir os dados na tabela
                string queryInsert = $"INSERT INTO Genero(Nome) Values(@Nome)";

                //abre conexão com o banco de dados
                con.Open();

                //instancia um objeto para utilizar dos metodos que permitem acesso ao sql
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {

                    cmd.Parameters.AddWithValue("@Nome", novoGenero.Nome);

                    //comando executara a query insert
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Deletar um genero existente
        /// </summary>
        /// <param name="id">id do genero a ser deletado</param>
        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //string de insert em TSQL que irá inserir os dados na tabela
                string queryDelete = $"DELETE FROM Genero WHERE IdGenero = @idgenero";

                //abre conexão com o banco de dados
                con.Open();

                //instancia um objeto para utilizar dos metodos que permitem acesso ao sql
                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {


                    cmd.Parameters.AddWithValue("@IdGenero", id);

                    //comando executara a query insert
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<GeneroDomain> ListarTodos()
        {
            List<GeneroDomain> ListaGenero = new List<GeneroDomain>();

            //Declara a sqlconnection utlizando a string conexao como parametro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // declara a instrução a ser utilizada no sql
                string querySelectAll = "SELECT IdGenero, Nome FROM Genero";

                // Abre a conexão com o banco de dados
                con.Open();

                // Para percorrer a tabela do banco
                SqlDataReader rdr;

                // Declara o sqlcommand que fará uma consulta utilizando o queryselectall e a conexão desejada
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        GeneroDomain genero = new GeneroDomain()
                        {
                            IdGenero = Convert.ToInt32(rdr[0]),

                            Nome = rdr["Nome"].ToString()

                        };

                        ListaGenero.Add(genero);

                    }
                }

            }

            return ListaGenero;
        }
    }
}
