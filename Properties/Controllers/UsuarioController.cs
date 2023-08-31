using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using webapi.Filmes.Properties.Domains;
using webapi.Filmes.Properties.Interfaces;
using webapi.Filmes.Properties.Repositories;

namespace webapi.Filmes.Properties.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpPost]
        public IActionResult Login(string Email, string Senha)
        {
            UsuarioDomain usuario = _usuarioRepository.Login(Email, Senha);

            try
            {
                if (usuario != null)
                {
                    if (usuario.Permissao == true)
                    {
                        return StatusCode(202, usuario);
                    }

                    return StatusCode(401, usuario);
                }

                return null;

            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }

}
