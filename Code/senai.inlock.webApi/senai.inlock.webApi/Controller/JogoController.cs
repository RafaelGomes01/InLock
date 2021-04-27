using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.inlock.webApi_.Domains;
using senai.inlock.webApi_.Interfaces;
using senai.inlock.webApi_.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi_.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        private Interfaces.IJogoRepository _jogoRepository { get; set; }

        public JogoController()
        {
            _jogoRepository = new JogoRepository();
        }

        [Authorize (Roles = "1")]
        [HttpPost]
        public IActionResult Create(JogoDomain novoJogo)
        {
            _jogoRepository.Create(novoJogo);
            return Created("http://localhost:500/api/Jogos", novoJogo);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Read()
        {
            return Ok(_jogoRepository.Read());
        }

        [HttpGet("{id}")]
        public IActionResult ReadById(int id)
        {
            if(_jogoRepository.ReadById(id) != null)
            {
                return Ok(_jogoRepository.ReadById(id));
            }

            return NotFound("Jogo não encontrado");
        }

        [HttpPut("{id}")]
        public IActionResult Update(JogoDomain jogoAtualizado, int id)
        {
            JogoDomain jogoBuscado = _jogoRepository.ReadById(id);

            if(jogoBuscado != null)
            {
                try
                {
                    _jogoRepository.Update(jogoAtualizado, id);
                    return NoContent();
                }

                catch (Exception erro)
                {
                    return BadRequest(erro);
                }
            }

            return NotFound("Jogo não encontrado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            JogoDomain jogoBuscado = _jogoRepository.ReadById(id);

            if(jogoBuscado != null)
            {
                _jogoRepository.Delete(id);
                return Ok("Deletado com Sucesso");
            }

            return NotFound("Jogo não encontrado");
        }

    }
}
