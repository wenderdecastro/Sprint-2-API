using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using webapi.Filmes.Properties.Domains;
using webapi.Filmes.Properties.Interfaces;
using webapi.Filmes.Properties.Repositories;

namespace webapi.Filmes.Properties.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private IFilmeRepository _filmeRepository { get; set; }

        public FilmeController()
        {
            _filmeRepository = new FilmeRepository();
        }

        /// <summary>
        /// EndPoint que lista todos os filmes existentes
        /// </summary>
        /// <returns>retorna uma lista com todos os filmes e o status code</returns>
        [HttpGet]
        [Authorize(Roles = "True,False")]
        public IActionResult Get()
        {

            _filmeRepository.ListarTodos();

            try
            {
                List<FilmeDomain> ListaFilmes = _filmeRepository.ListarTodos();

                return Ok(ListaFilmes);
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
            
        }

        /// <summary>
        /// EndPoint que consulta um filme pelo seu id
        /// </summary>
        /// <param name="id">filme a ser buscado</param>
        /// <returns>status code referente a operação e o objeto resultante da consulta</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "True,False")]
        public IActionResult GetbyID(int id)
        {
            try
            {
                //retorna a lista resultante da requisição e seu status code de ok
                if (_filmeRepository.BuscarPorId(id) == null)
                {
                    return StatusCode(404, "Objeto não encontrado.");
                }
                return Ok(_filmeRepository.BuscarPorId(id));

            }
            catch (Exception Erro)
            {
                //retorna um status code de erro
                return BadRequest(Erro.Message);
            }
        }


        /// <summary>
        /// EndPoint que cria um objeto filme
        /// </summary>
        /// <param name="novoFilme">Filme que será criado</param>
        /// <returns>retorna um status code com o resultado da operação</returns>
        [HttpPost]
        [Authorize(Roles = "True")]
        public IActionResult Create(FilmeDomain novoFilme)
        {
            try
            {
                _filmeRepository.Cadastrar(novoFilme);

                return StatusCode(201, novoFilme);
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// EndPoint que atualiza o filme por meio de um json
        /// </summary>
        /// <param name="filme">filme que sera alterado</param>
        /// <returns>Status code de acordo com o resultado da operação</returns>
        [HttpPut]
        [Authorize(Roles = "True")]
        public IActionResult EditFilmeBody(FilmeDomain filme)
        {
            try
            {
                FilmeDomain filmeBuscado = _filmeRepository.BuscarPorId(filme.IdFilme);

                if (filmeBuscado != null)
                {
                    try
                    {
                        _filmeRepository.AtualizarIdCorpo(filme);
                        return Ok();
                    }
                    catch (Exception Erro)
                    {
                        //retorna um status code de erro
                        return BadRequest(Erro.Message);
                    }
                }

                throw new Exception();
            }
            catch
            {
                return NotFound("Filme não encontrado");
            }

        }

        /// <summary>
        /// EndPoint que deleta um filme da tabela filmes
        /// </summary>
        /// <param name="id">id do filme a ser deletado</param>
        /// <returns>status code</returns>
        [HttpDelete]
        [Authorize(Roles = "True")]
        public IActionResult Delete(int id)
        {
            try
            {
                _filmeRepository.Deletar(id);

                return StatusCode(202, "Filme deletado com sucesso.");
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }

        }

        /// <summary>
        /// EndPoint que edita o filme pelo seu id
        /// </summary>
        /// <param name="id">Id do filme que será atualizado</param>
        /// <param name="filme">Objeto que sera alterado</param>
        /// <returns>Status code de acordo com o resultado da operação</returns>
        [HttpPatch("{id}")]
        [Authorize(Roles = "True")]
        public IActionResult EditFilmeById(int id, FilmeDomain filme)
        {
            try
            {
                FilmeDomain filmeBuscado = _filmeRepository.BuscarPorId(id);

                if (filmeBuscado != null)
                {
                    try
                    {
                        _filmeRepository.AtualizarIdUrl(id, filme);
                        return Ok();
                    }
                    catch (Exception Erro)
                    {
                        //retorna um status code de erro
                        return BadRequest(Erro.Message);
                    }
                }

                throw new Exception();
            }
            catch
            {
                return NotFound("Filme não encontrado");
            }
        }












    }

}
