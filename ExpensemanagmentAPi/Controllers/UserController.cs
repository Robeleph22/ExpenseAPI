using ExpensemanagmentAPi.Data;
using ExpensemanagmentAPi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExpensemanagmentAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("Success");
        }

        [HttpGet]
        public IActionResult GetUsers()
        {   
            return Ok(_context.Users.ToList());
        }
        
        [Authorize]
        [HttpGet("Id")]
        public IActionResult GetUser()
        {   
            return Ok(_context.Users.Find(User.UserId()));
        }
        
        [HttpPatch]
        public IActionResult UpdateUser(User newUserData)
        {
            newUserData.UserId = User.UserId();
            return Ok(_context.Users.Update(newUserData));
        }
    }
}
