using System.ComponentModel.DataAnnotations;

namespace ExpensemanagmentAPi.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }    
        public string? UserName { get; set; }
    }
}
