
using Expense_Tracker.Models;
using ExpensemanagmentAPi.Data;
using ExpensemanagmentAPi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpensemanagmentAPi.Controllers
{
    [Authorize()]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        //Get All
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAsync()
        {
            if (User.Identity?.Name != null)
            {
                return await _context.Categories.Where(c=>c.UserId == User.UserId()).ToListAsync();
            }
            else
            {
                return BadRequest();
            }
        }


        //Get By ID 

        [HttpGet("CategoryId")]
        [ProducesResponseType(typeof(Category) ,StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int CategoryId)
        {
            var Category = await _context.Categories.FirstAsync(c => c.CategoryId == CategoryId && c.UserId == User.UserId());
            return Category == null ? NotFound(): Ok(Category);
        }

        //Create Catagory Method 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task <IActionResult> Create(Category category)
        {
            category.UserId = User.UserId();
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),new {id = category.CategoryId} ,category);
        }

        //Update 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Category category)
        {
            category.UserId = User.UserId();
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }

        //Delete
        [HttpDelete("{CategoryId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(int CategoryId)
        {
            var categoryToDelete = await _context.Categories.FirstAsync(c=> c.CategoryId==CategoryId && c.UserId==User.UserId());
            if (categoryToDelete == null) return NotFound();

            _context.Categories.Remove(categoryToDelete);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
