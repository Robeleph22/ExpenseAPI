using Expense_Tracker.Models;
using ExpensemanagmentAPi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensemanagmentAPi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
    }
}



   
