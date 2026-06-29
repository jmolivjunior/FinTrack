
using Microsoft.AspNetCore.Mvc;
using FinTrack.API.Models;
using FinTrack.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FinTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionController(AppDbContext context)

        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Transaction>>> GetAll()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            return Ok(await _context.Transactions
                .Where(t => t.UserId == userId)
                .ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> Create(Transaction transaction)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            if (transaction.Installments > 1)
            {
                for (int i = 1; i <= transaction.Installments; i++)
                {
                    var installment = new Transaction
                    {
                        Description = $"{transaction.Description} ({i}/{transaction.Installments})",
                        Amount = transaction.Amount / transaction.Installments,
                        Category = transaction.Category,
                        Type = transaction.Type,
                        IsFixed = false,
                        Installments = transaction.Installments,
                        InstallmentNumber = i,
                        Date = DateTime.Now.AddMonths(i - 1),
                        UserId = userId
                    };
                    _context.Transactions.Add(installment);
                }
            }
            else
            {
                transaction.Date = DateTime.Now;
                transaction.UserId = userId;
                _context.Transactions.Add(transaction);
            }
            await _context.SaveChangesAsync();
            return Ok(transaction);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
                return NotFound();
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return Ok();
        }




    }

}