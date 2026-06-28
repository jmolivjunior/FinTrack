using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using FinTrack.API.Data;
using FinTrack.API.Models;


namespace FinTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BudgetController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BudgetController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Budget>>> GetAll()
        {
            return Ok(await _context.Budgets.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Budget>> Create(Budget budget)
        {
            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();
            return Ok(budget);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var budget = await _context.Budgets.FindAsync(id);
            if (budget == null)
                return NotFound();
            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
