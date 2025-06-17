using Microsoft.AspNetCore.Mvc;
using CryptoAPI.Models;
using CryptoAPI.Data;
using CryptoAPI.Services;
using Microsoft.EntityFrameworkCore;


namespace CryptoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly CryptoContext _context;
        private readonly CryptoYaService _criptoYaService;

        public TransactionsController(CryptoContext context, CryptoYaService criptoYaService)
        {
            _context = context;
            _criptoYaService = criptoYaService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CryptoTransaction transaccion)
        {
            if (transaccion.CryptoAmount <= 0)
                return BadRequest("Cantidad inválida");

            var price = await _criptoYaService.GetCryptoPrice(transaccion.CryptoCode);
            if (price == null)
                return BadRequest("Criptomoneda no válida");

            transaccion.Money = transaccion.CryptoAmount * price.Value;

            if (transaccion.Action == "sale")
            {
                var comprados = _context.Transactions
                    .Where(t => t.CryptoCode == transaccion.CryptoCode && t.Action == "purchase")
                    .Sum(t => t.CryptoAmount);

                var vendidos = _context.Transactions
                    .Where(t => t.CryptoCode == transaccion.CryptoCode && t.Action == "sale")
                    .Sum(t => t.CryptoAmount);

                if ((comprados - vendidos) < transaccion.CryptoAmount)
                    return BadRequest("No tenés suficientes criptomonedas.");
            }

            _context.Transactions.Add(transaccion);
            await _context.SaveChangesAsync();

            return Ok(transaccion);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CryptoTransaction>>> GetTransactions()
        {
            var transactions = await _context.Transactions
                .OrderByDescending(t => t.DateTime)
                .ToListAsync();

            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CryptoTransaction>> GetTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        public class UpdateTransactionDto
        {
            public decimal Money { get; set; }
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] UpdateTransactionDto updatedData)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
                return NotFound();

            transaction.Money = updatedData.Money;

            await _context.SaveChangesAsync();

            return Ok(transaction);
        }

        // DELETE: /transactions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
                return NotFound();

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 - Eliminado correctamente
        }


    }
}
