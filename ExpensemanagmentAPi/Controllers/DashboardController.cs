using Expense_Tracker.Models;
using ExpensemanagmentAPi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpensemanagmentAPi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetA()
        {

            var a = _context.Categories.Where(c => c.Type == "Expense"&& c.UserId==User.UserId()).SelectMany(c => c.Transactions).Sum(x => x.Amount);
            var b = _context.Categories.Where(c => c.Type == "Income" && c.UserId==User.UserId()).SelectMany(c => c.Transactions).Sum(x => x.Amount);
            var ff = _context.Categories.Join(_context.Transactions,
                p => p.CategoryId,
                c => c.CategoryId, ((category, transaction) => new
                {
                    //categoryName = category.Title,
                    //transactionName = transaction.Note,
                    amount = transaction.Amount,
                    date = transaction.Date,
                    typeId = transaction.Category.Type
                })
            ).ToList();

            var c = _context.Transactions.GroupBy(vg => vg.CategoryId).
                Select(vg => new
                {
                    //vg.Key,
                    //Transaction = vg,
                    Amount = vg.Sum(v => v.Amount),
                    //categoryId = vg.Select(v=>v.CategoryId).FirstOrDefault(),
                    categoryName = vg.Select(v => v.Category.Title).FirstOrDefault(),
                    categoryType = vg.Select(v => v.Category.Type).FirstOrDefault(),
                    count = vg.Select(v => v.CategoryId).Count(),

                }).ToList();



            DashboardData data = new DashboardData();
            ff.ForEach(e => { data.graphData.Add(e); });
            c.ForEach(e => { data.chartData.Add(e); });
            data.balance = b - a;
            data.totalExpens = a;
            data.totalIncome = b;

            data.user = _context.Users.Find(User.UserId());


            return Ok(data);
        }
    }
}
