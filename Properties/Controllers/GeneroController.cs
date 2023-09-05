using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Filmes.Properties.Domains;
using webapi.Filmes.Properties.Interfaces;
using webapi.Filmes.Properties.Repositories;

namespace webapi.Filmes.Properties.Controllers
{
    //define que a rota de uma requisição sera no seguinte formato
    //dominio/api/nomeController
    //ex: http://localhost:5000/api/genero
    [Route("api/[controller]")]
    //define que é um controlador de api
    [ApiController]
    //define que o tipo de resposta da api sera no formato JSON
    [Produces("application/json")]
    //metodo controlador, onde será criado os endpoints(rotas)
    public class GeneroController : ControllerBase
    {
        /// <summary>
        /// Objeto _GeneroRepository que ira receber todos os metodosdefinidos na interface
        /// </summary>
        private IGeneroRepository _generoRepository { get; set; }

        /// <summary>
        /// Instancia o objeto _generoRepository para referenciar os metodos no repositorio
        /// </summary>
        public GeneroController()
        {
            _generoRepository = new GeneroRepository();
        }

        /// <summary>
        /// EndPoint que aciona o metodo listar todos no repositorio e retorna a resposta para o usuario(front-end)
        /// </summary>
        /// <returns>retorna um status code de acordo com o resultado da operação</returns>
        [HttpGet]
        [Authorize(Roles = "True,False")]
        public IActionResult Get()
        {

            _generoRepository.ListarTodos();

            try
            {
                //cria uma lista que recebe os dados da requisição
                List<GeneroDomain> listaGeneros = _generoRepository.ListarTodos();

                //retorna a lista resultante da requisição e seu status code de ok
                return Ok(listaGeneros);
            }
            catch (Exception Erro)
            {
                //retorna um status code de erro
                return BadRequest(Erro.Message);
            }

        }

        /// <summary>
        /// EndPoint que consulta um genero pelo seu id
        /// </summary>
        /// <param name="id">genero a ser buscado</param>
        /// <returns>status code referente a operação e o objeto resultante da consulta</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "True,False")]
        public IActionResult GetbyID(int id)
        {
            try
            {
                //retorna a lista resultante da requisição e seu status code de ok
                if (_generoRepository.BuscarPorId(id) == null)
                {
                    return StatusCode(404, "Objeto não encontrado.");
                }
                return Ok(_generoRepository.BuscarPorId(id));

            }
            catch (Exception Erro)
            {
                //retorna um status code de erro
                return BadRequest(Erro.Message);
            }
        }

        /// <summary>
        /// EndPoint que aciona o metodo de cadastro
        /// </summary>
        /// <param name="novoGenero">Objeto recebido na requisição</param>
        /// <returns>status code 201 (created)</returns>
        [HttpPost]
        [Authorize(Roles = "True")]
        public IActionResult Post(GeneroDomain novoGenero)
        {

            try
            {
                _generoRepository.Cadastrar(novoGenero);

                return StatusCode(201, novoGenero);

            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }

        }

        /// <summary>
        /// EndPoint que atualiza o genero por meio de um json
        /// </summary>
        /// <param name="genero">genero que sera alterado</param>
        /// <returns>Status code de acordo com o resultado da operação</returns>
        [HttpPut]
        [Authorize(Roles = "True")]
        public IActionResult EditGeneroBody(GeneroDomain genero)
        {

            try
            {
                GeneroDomain generoBuscado = _generoRepository.BuscarPorId(genero.IdGenero);

                if (generoBuscado != null)
                {
                    try
                    {
                        _generoRepository.AtualizarIdCorpo(genero);
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
                return NotFound("Genero não encontrado");
            }

        }

        /// <summary>
        /// EndPoint que edita o genero pelo seu id
        /// </summary>
        /// <param name="id">Id do genero que será atualizado</param>
        /// <param name="genero">Objeto que sera alterado</param>
        /// <returns>Status code de acordo com o resultado da operação</returns>
        [HttpPatch("{id}")]
        [Authorize(Roles = "True")]
        public IActionResult EditGeneroById(int id, GeneroDomain genero)
        {
            try
            {
                GeneroDomain generoBuscado = _generoRepository.BuscarPorId(id);

                if (generoBuscado != null)
                {
                    try
                    {
                        _generoRepository.AtualizarIdUrl(id, genero);
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
                return NotFound("Genero não encontrado");
            }
        }

        /// <summary>
        /// EndPoint que deleta um genero existente
        /// </summary>
        /// <param name="id"> id do genero a ser deletado</param>
        /// <returns>status code referente a operação do metodo</returns>
        [HttpDelete]
        [Authorize(Roles = "True")]
        public IActionResult Delete(int id)
        {
            try
            {
                _generoRepository.Deletar(id);

                return Ok();
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }


        }





    }
}
