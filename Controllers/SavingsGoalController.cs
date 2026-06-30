using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FinTrack.API.Data;
using FinTrack.API.Models;

namespace FinTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SavingsGoalController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SavingsGoalController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SavingsGoal>>> GetAll()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            return Ok(await _context.SavingsGoals
                .Where(g => g.UserId == userId)
                .ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<SavingsGoal>> Create(SavingsGoal goal)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            goal.UserId = userId;
            _context.SavingsGoals.Add(goal);
            await _context.SaveChangesAsync();
            return Ok(goal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var goal = await _context.SavingsGoals.FindAsync(id);
            if (goal == null)
                return NotFound();
            _context.SavingsGoals.Remove(goal);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}