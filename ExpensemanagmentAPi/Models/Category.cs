using ExpensemanagmentAPi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }


        [Column(TypeName = "nvarchar(50)")]
        //Validation For Catagory
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }


        [Column(TypeName = "nvarchar(5)")]
        public string Icon { get; set; } = "💲";

        [Column(TypeName = "nvarchar(10)")]
        //setting default value as Expense
        public string Type { get; set; } = "Expense";

        [NotMapped]
        //Concatnating The Title and Icon 
        public string? TitleWithIcon
        {
            get
            {
                return this.Icon + " " + this.Title;
            }
        }

        public ICollection<Transaction>? Transactions { get; set; }

    }
}
