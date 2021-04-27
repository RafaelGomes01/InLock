using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai.inlock.webApi_.Domains;
using senai.inlock.webApi_.Interfaces;
using senai.inlock.webApi_.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace senai.inlock.webApi_.Controller
{
    [Produces("application/json")]
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
        public IActionResult Create(UsuarioDomain novoUsuario)
        {
            _usuarioRepository.Create(novoUsuario);
            return Created("http://localhost:5000/api/Usuarios", novoUsuario);
        }

        [HttpGet]
        public IActionResult Read()
        {
            return Ok(_usuarioRepository.Read());
        }

        [HttpGet("{id}")]
        public IActionResult ReadById(int id)
        {
            if (_usuarioRepository.ReadById(id) != null)
            {
                return Ok(_usuarioRepository.ReadById(id));
            }

            return NotFound("Usuario não encontrado");
        }

        [HttpPut("{id}")]
        public IActionResult Update(UsuarioDomain usuarioAtualizar, int id)
        {
            UsuarioDomain usuarioBuscado = _usuarioRepository.ReadById(id);

            if (usuarioBuscado != null)
            {
                try
                {
                    _usuarioRepository.Update(usuarioAtualizar, id);
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
            if (_usuarioRepository.ReadById(id) != null)
            {
                _usuarioRepository.Delete(id);
                return Ok("Deletado");
            }

            return NotFound("Usuario não encontrado");


        }

        [HttpPost("Login")]
        public IActionResult Login(UsuarioDomain login)
        {
            UsuarioDomain usuarioBuscado = _usuarioRepository.Login(login.email, login.senha);

            if(usuarioBuscado == null)
            {
                return NotFound("Dado incorretos");
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.email),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.idUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuarioBuscado.idTipoUsuario.ToString())               
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("games-chave-autenticacao"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "Inlock.webApi",
                audience: "Inlock.webApi",
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: creds
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
