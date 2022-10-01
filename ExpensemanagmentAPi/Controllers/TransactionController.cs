using Expense_Tracker.Models;
using ExpensemanagmentAPi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpensemanagmentAPi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        //Get All
        [HttpGet]
        public async Task<IEnumerable<Transaction>> GetAsync()
        {
            return await _context.Transactions.Where(c=>c.Category.UserId == User.UserId()).ToListAsync();
        }


        //Get By ID 

        [HttpGet("TransactionId")]
        [ProducesResponseType(typeof(Transaction), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int TransactionId)
        {
            var Transaction = await _context.Transactions.FirstAsync(c=>c.TransactionId==TransactionId && c.Category.UserId == User.UserId());
            return Transaction == null ? NotFound() : Ok(Transaction);

        }

        //Create Transaction Method 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
 
            return CreatedAtAction(nameof(GetById), new { id = transaction.TransactionId }, transaction);
        }



      


        //Update 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update( Transaction transaction)
        {
            _context.Entry(transaction).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Delete
        [HttpDelete("{TransactionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(int TransactionId)
        {
            var transactionToDelete = await _context.Transactions.FirstAsync(c=>TransactionId==c.TransactionId && c.Category.UserId == User.UserId());
            if (transactionToDelete == null) return NotFound();

            _context.Transactions.Remove(transactionToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
