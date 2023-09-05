using System.Data.SqlClient;
using webapi.Filmes.Properties.Domains;
using webapi.Filmes.Properties.Interfaces;

namespace webapi.Filmes.Properties.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private string StringConexao = "Data Source=NOTE10-S15; Initial Catalog=Filmes; User ID =sa; Pwd =Senai@134";

        public UsuarioDomain Login(string email, string senha)
        {

            string queryUsuario = $"SELECT IdUsuario, Email, Permissao FROM Usuario WHERE Email = @Email AND Senha = @Senha";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryUsuario, con))
                {

                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Senha", senha);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {

                        if (rdr.Read())
                        {

                            UsuarioDomain usuario = new UsuarioDomain()
                            {
                                IdUsuario = Convert.ToInt32(rdr["IdUsuario"]),
                                Email = rdr["Email"].ToString(),
                                //Senha = rdr["Senha"].ToString(),
                                Permissao = rdr.GetBoolean(2)
                            };

                            return usuario;

                        }

                        return null;

                    }
                }
            }
        }
    }
}
