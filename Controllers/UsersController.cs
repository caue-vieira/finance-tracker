using finance_tracker.Context;
using finance_tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace finance_tracker.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class UsersController : Controller {
        // Criando uma instância do contexto do banco de dados
        private TrackerDbContext _context;

        // Construtor do controlador com contexto do banco de dados
        public UsersController(TrackerDbContext context) {
            _context = context;
        }

        [HttpPost("signup")]
        public IActionResult CadastraUsuario(User user) {
            try {
                if(user is null) {
                    return BadRequest("O usuário não pôde ser criado");
                }

                user.UserId = Guid.NewGuid();

                _context.Users.Add(user);
                _context.SaveChanges();

                return Created(string.Empty, user);
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro ao criar o usuário");
            }
        }

        [HttpGet("{id:Guid}")]
        public IActionResult UserById(Guid id) {
            try {
                var usuario = _context.Users.FirstOrDefault(usuario => usuario.UserId == id);
                if (usuario is null) {
                    return NotFound("Usuário não encontrado");
                }
                return Ok(usuario);
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro ao buscar o usuário");
            }
        }

        [HttpPut("{id:Guid}/update")]
        public IActionResult UpdateUserById(Guid id, User user)
        {
            try
            {
                if(id != user.UserId)
                {
                    return BadRequest("ID de usuário inválido");
                }
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok();
            } catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro ao buscar o usuário");
            }
        }

        [HttpDelete("{id:Guid}/delete")]
        public IActionResult DeleteUserById(Guid id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(user => user.UserId == id);
                if (user is null)
                {
                    return NotFound("Usuário não encontrado");
                }
                _context.Users.Remove(user);
                _context.SaveChanges();
                return Ok();
            } catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro ao buscar o usuário");
            }
        }
    }
}
