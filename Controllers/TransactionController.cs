
using Microsoft.AspNetCore.Mvc;
using FinTrack.API.Models;

namespace FinTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private static List<Transaction> transactions = new List<Transaction>();

        [HttpGet]
        public ActionResult<List<Transaction>> GetAll()
        {
            return Ok(transactions);
        }

        [HttpPost]
        public ActionResult<Transaction> Create(Transaction transaction)
        {
            transaction.Id = transactions.Count + 1;
            transaction.Date = DateTime.Now;
            transactions.Add(transaction);
            return Ok(transaction);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var transaction = transactions.FirstOrDefault(t => t.Id == id);
            if (transaction == null) 
            return NotFound();
            transactions.Remove(transaction);
            return Ok();
        }
    }
}