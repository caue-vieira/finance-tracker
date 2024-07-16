using finance_tracker.Context;
using finance_tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace finance_tracker.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class TransactionsController : Controller {
        private TrackerDbContext _context;
        
        public TransactionsController(TrackerDbContext context) {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult CadastraTransacao(Transaction transaction) {
            try {
                if(transaction is null) {
                    return BadRequest("A transação não pôde ser criada");
                }
                transaction.Id = Guid.NewGuid();

                _context.Transaction.Add(transaction);
                _context.SaveChanges();

                return Created(string.Empty, transaction);
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro ao criar a transação");
            }
        }

        [HttpPut("{id:Guid}/update")]
        public IActionResult AlteraTransacao(Guid id, Transaction transacao) {
            try {
                if(id != transacao.Id) {
                    return BadRequest("Id de transação inválido");
                }

                _context.Entry(transacao).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok();
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro ao atualizar a transação");
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Transaction>> TransacaoByIdAsync(Guid id) {
            try {
                var transacao = await _context.Transaction.FirstOrDefaultAsync(t => t.Id == id);
                if(transacao is null) {
                    return NotFound("Transação não encontrada");
                }
                return transacao;
            } catch(Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro ao buscar a transação");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Transaction>> ListarTransacoes(Guid userId) {
            try {
                return _context.Transaction
                    .Where(transactions => transactions.UserId == userId)
                    .ToList();
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro ao listar as categorias");
            }
        }

        [HttpDelete("{id:Guid}/delete")]
        public ActionResult DeletaTransacao(Guid id) {
            try {
                var transacao = _context.Transaction.FirstOrDefault(t => t.Id == id);
                if(transacao is null) {
                    return NotFound("Transação não encontrada");
                }

                _context.Transaction.Remove(transacao);
                _context.SaveChanges();

                return Ok();
            } catch(Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro ao excluir a transação");
            }
        }
    }
}
