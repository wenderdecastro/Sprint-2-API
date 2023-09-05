using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using webapi.Filmes.Properties.Domains;
using webapi.Filmes.Properties.Interfaces;
using webapi.Filmes.Properties.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace webapi.Filmes.Properties.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpPost]
        public IActionResult Login(UsuarioDomain usuario)
        {
            UsuarioDomain usuarioBuscado = _usuarioRepository.Login(usuario.Email, usuario.Senha);

            try
            {
                if (usuario != null)
                {
                    //if (usuario.Permissao == true)
                    //{

                    //criação do token JWT 

                    //1º, define as informações do token (payload)

                    var claims = new[]
                    {
                            new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                            new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                            new Claim(ClaimTypes.Role, usuarioBuscado.Permissao.ToString()),

                            // new Claim("claim personalizada", "valor da claim personalizada")
                        };

                    //definir chave de acesso ao token

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("key-filmes.webapi.auth.dev-senai"));

                    // definir as credenciais do token

                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        // emissor do token
                        issuer: "webapi.Filmes",
                        // destinatario do token
                        audience: "webapi.Filmes",
                        // informações do token
                        claims: claims,
                        // duração do token
                        expires: DateTime.Now.AddMinutes(30),
                        // credenciais que serão utilizadas
                        signingCredentials: creds
                        );


                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token)
                    });



                    //Console.WriteLine("Usuario administrador");
                    //return StatusCode(202, usuarioBuscado);
                }

                //Console.WriteLine("Usuario Comum");
                //return StatusCode(202, usuarioBuscado);

                //}

                return NotFound("Email ou senha inválidos");

            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }

}
